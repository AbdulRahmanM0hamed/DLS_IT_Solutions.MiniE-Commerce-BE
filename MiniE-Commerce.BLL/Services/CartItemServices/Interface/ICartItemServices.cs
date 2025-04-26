using MiniE_Commerce.BLL.Helper.Response;
using MiniE_Commerce.BLL.Services.CartItemServices.Model;

namespace MiniE_Commerce.BLL.Services.CartItemServices.Interface
{
    public interface ICartItemServices
    {
        Task<ResponseModel<string>> AddCartItemAsync(AddCartItemDto cartItem);
        Task<ResponseModel<string>> DeleteCartItemAsync(Guid id);
        Task<ResponseModel<IReadOnlyList<GetCartItemDto>>> GetAllCartItemsByUserAsync(string userId);
        Task<ResponseModel<GetCartItemDto>> GetCartItemByIdAsync(Guid id);
        Task<ResponseModel<string>> UpdateCartItemAsync(UpdateCartItemDto cartItem);
    }
}
