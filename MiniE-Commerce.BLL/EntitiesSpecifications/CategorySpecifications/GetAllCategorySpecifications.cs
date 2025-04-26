using MiniE_Commerce.BLL.EntitiesSpecifications.Base;
using MiniE_Commerce.BLL.Specifications;
using MiniE_Commerce.DAL.Entities.CategoryEntity;

namespace MiniE_Commerce.BLL.EntitiesSpecifications.CategorySpecifications
{

    public class GetAllCategorySpecifications : BaseSpecifications<Category>
    {
        public GetAllCategorySpecifications(BaseSpecificationParams specificationParams) : base(p => true)
        {
            if (specificationParams != null)
                ApplyPaging((specificationParams.PageIndex - 1) * specificationParams.PageSize, specificationParams.PageSize);
            AddInclude(x => x.Products);
        }
    }
}
