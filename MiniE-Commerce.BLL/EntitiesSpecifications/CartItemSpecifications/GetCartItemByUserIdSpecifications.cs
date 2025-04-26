using MiniE_Commerce.BLL.Specifications;
using MiniE_Commerce.DAL.Entities.CartItemEntity;

namespace MiniE_Commerce.BLL.EntitiesSpecifications.CartItemSpecifications
{
    public class GetCartItemByUserIdSpecifications : BaseSpecifications<CartItem>
    {
        public GetCartItemByUserIdSpecifications(string userId) : base(c => c.UserId == userId)
        {
            AddInclude(x => x.Product);
            AddInclude(x => x.User);
        }
    }
}
