using Microsoft.EntityFrameworkCore;
using MiniE_Commerce.DAL.Entities.BaseEntities;

namespace MiniE_Commerce.DAL.Entities.User
{
    [Owned]
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public DateTime? RevokedOn { get; set; }
        public bool IsActive => RevokedOn == null && !IsExpired;
    }
}
