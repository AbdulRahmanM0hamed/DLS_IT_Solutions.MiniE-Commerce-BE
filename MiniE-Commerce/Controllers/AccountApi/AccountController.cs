using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniE_Commerce.BLL.Helper.Global;
using MiniE_Commerce.BLL.Helper.Response;
using MiniE_Commerce.BLL.Services.Auth.Interface;
using MiniE_Commerce.BLL.Services.Auth.Models;
using MiniE_Commerce.DAL.Entities.Enums;
using MiniE_Commerce.DAL.Entities.User;
using MiniE_Commerce.Errors;
using System.Transactions;

namespace MiniE_Commerce.Controllers.AccountApi
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AccountController(UserManager<ApplicationUser> userManager, IMapper mapper, IAuthService authService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _authService = authService;
        }

        [HttpPost("Register-For-User")]
        public async Task<IActionResult> Register([FromBody] AuthRegisterModel model)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var result = await _authService.RegisterAsync(model);

                    if (!result.IsAuthenticated)
                        return BadRequest(new ApiResponse(CodeStatus.BadRequest, "user registration failed: " + result.Message));

                    transaction.Complete();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(new ApiResponse(CodeStatus.BadRequest, "An error occurred during registration. "));
                }
            }
        }



        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsUser([FromBody] AuthLoginModel loginDto)
        {
            if (!IsValidEmail(loginDto.Email))
                return NotFound(ResponseModel<string>.Error("User Email Not Found"));

            var result = await _authService.LoginAsync(loginDto);
            if (result == null || string.IsNullOrEmpty(result.Email))
                return BadRequest(ResponseModel<string>.Error("Email Or Password Invalid"));

            return Ok(result);
        }


        [Authorize]
        [HttpPost("LogOut")]
        public async Task<IActionResult> SignOut([FromBody] string refreshToken)
        {
            var revokeTokenResult = await _authService.RevokeTokenAsync(refreshToken);

            if (revokeTokenResult)
                return Ok(ResponseModel<string>.Success("Success"));

            return BadRequest(ResponseModel<string>.Error("Failed To Log Out User Please Try Again"));
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }

}
