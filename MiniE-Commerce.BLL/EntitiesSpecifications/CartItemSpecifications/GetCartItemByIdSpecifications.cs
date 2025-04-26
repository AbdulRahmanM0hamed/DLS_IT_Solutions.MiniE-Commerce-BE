using MiniE_Commerce.BLL.Specifications;
using MiniE_Commerce.DAL.Entities.CartItemEntity;

namespace MiniE_Commerce.BLL.EntitiesSpecifications.CartItemSpecifications
{
    public class GetCartItemByIdSpecifications : BaseSpecifications<CartItem>
    {
        public GetCartItemByIdSpecifications(Guid Id) : base(c => c.Id == Id)
        {
            AddInclude(x => x.Product);
            AddInclude(x => x.User);
        }
    }
}
