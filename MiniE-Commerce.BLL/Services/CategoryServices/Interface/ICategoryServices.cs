using MiniE_Commerce.BLL.EntitiesSpecifications.Base;
using MiniE_Commerce.BLL.Helper.Response;
using MiniE_Commerce.BLL.Services.CategoryServices.Model;

namespace MiniE_Commerce.BLL.Services.CategoryServices.Interface
{
    public interface ICategoryServices
    {
        Task<ResponseModel<IReadOnlyList<GetCategoryDto>>> GetAllAsync(BaseSpecificationParams specParams);
        Task<ResponseModel<GetCategoryByIdDto>> GetByIdAsync(Guid id);
        Task<ResponseModel<string>> AddAsync(AddCategoryDto categoryDto);
        Task<ResponseModel<string>> UpdateAsync(UpdateCategoryDto categoryDto);
        Task<ResponseModel<string>> DeleteAsync(Guid id);
    }
}
