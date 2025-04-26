using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiniE_Commerce.BLL.Helper.Global;
using MiniE_Commerce.BLL.Services.Auth.Interface;
using MiniE_Commerce.BLL.Services.Auth.Models;
using MiniE_Commerce.DAL.Entities.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MiniE_Commerce.BLL.Services.Auth
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<AuthModel> LoginAsync(AuthLoginModel model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.Users.FirstOrDefaultAsync(p => p.Email == model.Email);
            var checkPass = await _userManager.CheckPasswordAsync(user, model.Password);
            if (user is null || !checkPass)
            {
                authModel.Message = "Email or password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();

            var previousActiveRefreshToken = user.RefreshTokens!.SingleOrDefault(t => t.IsActive);
            if (previousActiveRefreshToken != null)
            {
                previousActiveRefreshToken.RevokedOn = DateTime.UtcNow;
                var newRefreshToken = GenerateRefreshToken(model.Email);
                authModel.RefreshToken = newRefreshToken.Token;
                authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;
                user.RefreshTokens!.Add(newRefreshToken);
                await _userManager.UpdateAsync(user);
            }
            else
            {
                var newRefreshToken = GenerateRefreshToken(model.Email);
                authModel.RefreshToken = newRefreshToken.Token;
                authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;
                user.RefreshTokens!.Add(newRefreshToken);
                await _userManager.UpdateAsync(user);
            }

            return authModel;
        }

        public async Task<AuthModel> RegisterAsync(AuthRegisterModel model)
        {

            if (!string.IsNullOrEmpty(model.Email) && await _userManager.Users.AnyAsync(p => p.Email == model.Email))
                return new AuthModel { Message = " email is already in use" };

            if (!string.IsNullOrEmpty(model.PhoneNumber) && await _userManager.Users.AnyAsync(p => p.PhoneNumber == model.PhoneNumber))
                return new AuthModel { Message = " phone number is already in use" };


            if (await _roleManager.FindByNameAsync(Roles.User) is null)
                return new AuthModel { Message = $"There is no role with this name user" };

            Random rnd = new Random();
            var username = $"{model.Email}{rnd.Next()}";

            if (await _userManager.FindByNameAsync(username) is not null)
                return new AuthModel { Message = "Username is already in use" };

            var user = new ApplicationUser
            {
                FirstName = model.FristName,
                LastName = model.LastName,
                UserName = username,
                Email = string.IsNullOrEmpty(model.Email) ? username + "@MiniE_Commerce.com" : model.Email,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthModel { Message = errors };
            }

            await AddUserToRoleAsync(username, Roles.User);

            var jwtSecurityToken = await CreateJwtToken(user);

            var refreshToken = GenerateRefreshToken(model.FristName);
            user.RefreshTokens!.Add(refreshToken);
            await _userManager.UpdateAsync(user);

            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { Roles.User },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpiresOn
            };
        }
        public async Task<string> AddUserToRoleAsync(string username, string roleName)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user is null || !await _roleManager.RoleExistsAsync(roleName))
                return "Invalid user ID or RoleName";

            if (await _userManager.IsInRoleAsync(user, roleName))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, roleName);

            return result.Succeeded ? "user added to role successfully" : "Something went wrong";
        }

        public async Task<AuthModel> RefreshTokenAsync(string token)
        {
            var authModel = new AuthModel();

            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens!.Any(t => t.Token == token));

            if (user == null)
            {
                authModel.Message = "invalid token";
                return authModel;
            }

            var refreshToken = user.RefreshTokens!.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
            {
                authModel.Message = "inactive token";
                return authModel;
            }

            var jwtToken = await CreateJwtToken(user);
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            authModel.Username = user.UserName;
            authModel.Email = user.PhoneNumber;
            var roles = await _userManager.GetRolesAsync(user);
            authModel.Roles = roles.ToList();
            authModel.RefreshToken = refreshToken.Token;
            authModel.RefreshTokenExpiration = refreshToken.ExpiresOn;
            authModel.ExpiresOn = jwtToken.ValidTo;

            return authModel;
        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens!.Any(t => t.Token == token));

            if (user == null)
                return false;

            var refreshToken = user.RefreshTokens!.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
                return false;
            refreshToken.RevokedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);
            return true;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudiance"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:DurationInMinutes"]!)),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken(string username)
        {
            var randomNumber = new byte[32];

            using var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            Random rnd = new Random();

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(30),
            };
        }
    }
}
