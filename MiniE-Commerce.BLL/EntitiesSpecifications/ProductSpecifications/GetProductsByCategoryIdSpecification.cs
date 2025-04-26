using MiniE_Commerce.BLL.Specifications;
using MiniE_Commerce.DAL.Entities.ProductEntity;

namespace MiniE_Commerce.BLL.EntitiesSpecifications.ProductSpecifications
{

    public class GetProductsByCategoryIdSpecification : BaseSpecifications<Product>
    {
        public GetProductsByCategoryIdSpecification(Guid CategoryId) : base(p => p.CategoryId == CategoryId)
        {
            
        }
    }
}
