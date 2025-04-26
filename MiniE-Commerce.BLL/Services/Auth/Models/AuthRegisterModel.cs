using MiniE_Commerce.DAL.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace MiniE_Commerce.BLL.Services.Auth.Models
{
    public class AuthRegisterModel
    {
        [Required(ErrorMessage = "first name is required")]
        [StringLength(128)] public string FristName { get; set; }

        [Required(ErrorMessage = "last name is required")]
        [StringLength(128)] public string LastName { get; set; }

        [Required(ErrorMessage = "email is required")]
        [EmailAddress]
        [StringLength(128)]
        public string Email { get; set; }

        [Required(ErrorMessage = "phone number is required")]
        [Phone(ErrorMessage = "Please enter only Numbers")]
        [StringLength(maximumLength: 11, MinimumLength = 11, ErrorMessage = "The Phone Number Must be of Length 11")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "password is required")]
        [StringLength(128)] public string Password { get; set; }

        [Required(ErrorMessage = "gender is required")]
        public Gender Gender { get; set; }
    }
}
