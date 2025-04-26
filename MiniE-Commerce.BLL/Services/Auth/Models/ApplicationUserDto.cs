using MiniE_Commerce.DAL.Entities.Enums;

namespace MiniE_Commerce.BLL.Services.Auth.Models
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? ImageUrl { get; set; }
        public Gender Gender { get; set; }
     
    }
}
