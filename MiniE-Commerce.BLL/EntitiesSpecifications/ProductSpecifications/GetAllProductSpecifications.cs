using MiniE_Commerce.BLL.EntitiesSpecifications.Base;
using MiniE_Commerce.BLL.Specifications;
using MiniE_Commerce.DAL.Entities.ProductEntity;

namespace MiniE_Commerce.BLL.EntitiesSpecifications.ProductSpecifications
{
    public class GetAllProductSpecifications : BaseSpecifications<Product>
    {
        public GetAllProductSpecifications() : base(p => true)
        {
            AddInclude(x => x.Category);
        }
    }
}
