using AutoMapper;
using MiniE_Commerce.BLL.EntitiesSpecifications.CartItemSpecifications;
using MiniE_Commerce.BLL.Helper.Response;
using MiniE_Commerce.BLL.Interfaces;
using MiniE_Commerce.BLL.Services.CartItemServices.Interface;
using MiniE_Commerce.BLL.Services.CartItemServices.Model;
using MiniE_Commerce.DAL.Entities.CartItemEntity;

namespace MiniE_Commerce.BLL.Services.CartItemServices
{

    public class CartItemService : ICartItemServices
    {
        private readonly IGenericRepository<CartItem> _repository;
        private readonly IMapper _mapper;

        public CartItemService(IGenericRepository<CartItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<string>> AddCartItemAsync(AddCartItemDto dto)
        {
            var cartItem = _mapper.Map<CartItem>(dto);
            await _repository.CreateEntityAsync(cartItem);
            await _repository.SaveChangesAsync();
            return ResponseModel<string>.Success("Item added to cart.");
        }

        public async Task<ResponseModel<string>> DeleteCartItemAsync(Guid id)
        {
            var cartItem = await _repository.GetEntityById(id);
            if (cartItem == null)
                return ResponseModel<string>.Error("Item not found.");

            _repository.DeleteEntityAsync(cartItem);
            await _repository.SaveChangesAsync();
            return ResponseModel<string>.Success("Item removed from cart.");
        }

        public async Task<ResponseModel<IReadOnlyList<GetCartItemDto>>> GetAllCartItemsByUserAsync(string userId)
        {
            var items = await _repository.GetAllEntitiesWithSpec(new GetCartItemByUserIdSpecifications(userId));
            if (!items.Any()) return ResponseModel<IReadOnlyList<GetCartItemDto>>.Error("Cart is empty.");
            var mapped = _mapper.Map<IReadOnlyList<GetCartItemDto>>(items);
            return ResponseModel<IReadOnlyList<GetCartItemDto>>.Success(mapped);
        }              

        public async Task<ResponseModel<GetCartItemDto>> GetCartItemByIdAsync(Guid id)
        {
            var item = await _repository.GetEntityById(id);
            if (item == null) return ResponseModel<GetCartItemDto>.Error("Item not found.");
            var mapped = _mapper.Map<GetCartItemDto>(item);
            return ResponseModel<GetCartItemDto>.Success(mapped);
        }

        public async Task<ResponseModel<string>> UpdateCartItemAsync(UpdateCartItemDto dto)
        {
            var item = await _repository.GetEntityById(dto.Id);
            if (item == null) return ResponseModel<string>.Error("Item not found.");

            item.Quantity = dto.Quantity;
            await _repository.UpdateEntityAsync(item);
            await _repository.SaveChangesAsync();
            return ResponseModel<string>.Success("Cart updated.");
        }

    }

}
