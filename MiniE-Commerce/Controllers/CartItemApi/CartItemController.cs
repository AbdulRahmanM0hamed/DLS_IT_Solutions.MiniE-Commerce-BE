using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniE_Commerce.BLL.Helper.Global;
using MiniE_Commerce.BLL.Services.CartItemServices.Interface;
using MiniE_Commerce.BLL.Services.CartItemServices.Model;

namespace MiniE_Commerce.Controllers.CartItemApi
{
    public class CartItemController : BaseController
    {
        private readonly ICartItemServices _cartItemService;
        public CartItemController(ICartItemServices cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpGet("Get-All-CartItems-By-user-Id")]
        public async Task<IActionResult> GetAllCartItemsByUserId( string userId)
        {
            var result = await _cartItemService.GetAllCartItemsByUserAsync(userId);
            return result.Ok ? Ok(result) : NotFound(result);
        }

        [HttpGet("Get-CartItem-By-Id")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _cartItemService.GetCartItemByIdAsync(id);
            return result.Ok ? Ok(result) : NotFound(result);
        }
        
        [HttpPost("Add-CartItem")]
        public async Task<IActionResult> AddCartItem([FromBody] AddCartItemDto dtos)
        {
            var result = await _cartItemService.AddCartItemAsync(dtos);
            return result.Ok ? Ok(result) : BadRequest(result);
        }
        

        [HttpPut("Update-CartItem")]
        public async Task<IActionResult> UpdateCartItem([FromBody] UpdateCartItemDto dto)
        {
            var result = await _cartItemService.UpdateCartItemAsync(dto);
            return result.Ok ? Ok(result) : BadRequest(result);
        }
       

        [HttpDelete("Delete-CartItem")]
        public async Task<IActionResult> DeleteCartItem(Guid id)
        {
            var result = await _cartItemService.DeleteCartItemAsync(id);
            return result.Ok ? Ok(result) : BadRequest(result);
        }
    }
}
