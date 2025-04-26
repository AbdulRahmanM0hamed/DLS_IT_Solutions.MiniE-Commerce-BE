using MiniE_Commerce.BLL.Services.Auth.Models;

namespace MiniE_Commerce.BLL.Services.Auth.Interface
{
    public interface IAuthService
    {
        public Task<AuthModel> RegisterAsync(AuthRegisterModel model);
        public Task<AuthModel> LoginAsync(AuthLoginModel model);   
        public Task<AuthModel> RefreshTokenAsync(string token);
        public Task<string> AddUserToRoleAsync(string username, string roleName);
        public Task<bool> RevokeTokenAsync(string token);  
    }
}
