using Microsoft.AspNetCore.Mvc;
using MiniE_Commerce.BLL.EntitiesSpecifications.Base;
using MiniE_Commerce.BLL.Services.ProductServices.Models;
using MiniE_Commerce.BLL.Services.ProductServices;
using MiniE_Commerce.BLL.Services.ProductServices.Interface;
using Microsoft.AspNetCore.Authorization;
using MiniE_Commerce.BLL.Helper.Global;
using MiniE_Commerce.BLL.Helper.Response;

namespace MiniE_Commerce.Controllers.ProductApi
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("Get-All-Products")]
        public async Task<IActionResult> GetAllProducts()        
        {
            var result = await _productService.GetAllProductsAsync();
            return result.Ok == true ? Ok(result) : NotFound(result);
        }

        [HttpGet("Get-Product-By-Id")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            return result.Ok == true ? Ok(result) : NotFound(result);
        }

        [HttpGet("Get-All-Products-By-Category-Id")]
        public async Task<IActionResult> GetAllProductsByCategoryId([FromRoute] Guid categoryId)
        {
            var result = await _productService.GetProductsByCategoryIdAsync(categoryId);
            return result.Ok == true ? Ok(result) : NotFound(result);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("Create-Product")]
        public async Task<IActionResult> CreateProduct([FromBody] AddProductsDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseModel<string>.Error("InvalidTripData"));

            var result = await _productService.AddProductAsync(dto);
            return result.Ok == true ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("Update-Product")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductsDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseModel<string>.Error("InvalidTripData"));

            var result = await _productService.UpdateProductAsync(dto);
            return result.Ok == true ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("Delete-Product")]
        public async Task<IActionResult> DeleteProduct( Guid id)
        {
            var result = await _productService.DeleteProductAsync(id);
            return result.Ok == true ? Ok(result) : BadRequest(result);
        }
    }
}
