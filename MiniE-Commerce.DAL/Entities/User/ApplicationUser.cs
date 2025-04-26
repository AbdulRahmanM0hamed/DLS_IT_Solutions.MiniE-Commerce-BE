using Microsoft.AspNetCore.Identity;
using MiniE_Commerce.DAL.Entities.Enums;

namespace MiniE_Commerce.DAL.Entities.User
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public ICollection<RefreshToken>? RefreshTokens { get; set; }
        public string GetUserFullName()
            => this.FirstName + " " + this.LastName;
    }
}
