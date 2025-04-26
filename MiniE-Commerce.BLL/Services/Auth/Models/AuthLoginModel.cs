using System.ComponentModel.DataAnnotations;

namespace MiniE_Commerce.BLL.Services.Auth.Models
{
    public class AuthLoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress] public string Email { get; set; }

        [Required(ErrorMessage = "password is required")]
        [StringLength(128)] public string Password { get; set; }
      
    }
}
