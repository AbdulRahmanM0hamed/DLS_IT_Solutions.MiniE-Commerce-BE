using MiniE_Commerce.DAL.Entities.BaseEntities;
using MiniE_Commerce.DAL.Entities.ProductEntity;
using MiniE_Commerce.DAL.Entities.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniE_Commerce.DAL.Entities.CartItemEntity
{
    public class CartItem : BaseEntity
    {
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public int Quantity { get; set; }
    } 
}