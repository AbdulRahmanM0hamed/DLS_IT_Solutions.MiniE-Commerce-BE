using MiniE_Commerce.BLL.EntitiesSpecifications.Base;
using MiniE_Commerce.BLL.Specifications;
using MiniE_Commerce.DAL.Entities.ProductEntity;

namespace MiniE_Commerce.BLL.EntitiesSpecifications.ProductSpecifications
{
    public class GetProductByIdSpecifications : BaseSpecifications<Product>
    {
        public GetProductByIdSpecifications(Guid productId) : base(p => p.Id == productId)
        {
            AddInclude(x => x.Category);
        }
    }
}
