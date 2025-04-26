using MiniE_Commerce.BLL.EntitiesSpecifications.Base;
using MiniE_Commerce.BLL.Helper.Response;
using MiniE_Commerce.BLL.Services.ProductServices.Models;

namespace MiniE_Commerce.BLL.Services.ProductServices.Interface
{
    public interface IProductService
    {
        Task<ResponseModel<IReadOnlyList<GetProductsDto>>> GetAllProductsAsync();
        Task<ResponseModel<GetProductByIdDto>> GetProductByIdAsync(Guid id);
        Task<ResponseModel<IReadOnlyList<GetProductsDto>>> GetProductsByCategoryIdAsync(Guid categoryId);
        Task<ResponseModel<string>> AddProductAsync(AddProductsDto product);
        Task<ResponseModel<string>> UpdateProductAsync(UpdateProductsDto product);
        Task<ResponseModel<string>> DeleteProductAsync(Guid id);
    }
}
