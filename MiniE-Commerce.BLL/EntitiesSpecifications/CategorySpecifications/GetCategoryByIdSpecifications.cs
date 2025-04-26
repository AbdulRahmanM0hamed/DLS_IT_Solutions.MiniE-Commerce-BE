using MiniE_Commerce.BLL.Specifications;
using MiniE_Commerce.DAL.Entities.CategoryEntity;

namespace MiniE_Commerce.BLL.EntitiesSpecifications.CategorySpecifications
{
    public class GetCategoryByIdSpecifications : BaseSpecifications<Category>
    {
        public GetCategoryByIdSpecifications(Guid categoryId) : base(p => p.Id == categoryId)
        {
            AddInclude(x => x.Products);
        }
    }
}
