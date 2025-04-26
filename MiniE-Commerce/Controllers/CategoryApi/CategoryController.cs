using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniE_Commerce.BLL.EntitiesSpecifications.Base;
using MiniE_Commerce.BLL.Helper.Global;
using MiniE_Commerce.BLL.Helper.Response;
using MiniE_Commerce.BLL.Services.CategoryServices.Interface;
using MiniE_Commerce.BLL.Services.CategoryServices.Model;

namespace MiniE_Commerce.Controllers.CategoryApi
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryServices _categoryService;

        public CategoryController(ICategoryServices categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("Get-All-Categories")]
        public async Task<IActionResult> GetAllCategories([FromQuery] BaseSpecificationParams @params)
        {
            var result = await _categoryService.GetAllAsync(@params);
            return result.Ok ? Ok(result) : NotFound(result);
        }

        [HttpGet("Get-Category-By-Id")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            return result.Ok ? Ok(result) : NotFound(result);
        }
        
        [Authorize(Roles = Roles.Admin)]
        [HttpPost("Add-Category")]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseModel<string>.Error("Invalid data."));

            var result = await _categoryService.AddAsync(dto);
            return result.Ok ? Ok(result) : BadRequest(result);
        }
        
        [Authorize(Roles = Roles.Admin)]
        [HttpPut("Update-Category")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseModel<string>.Error("Invalid data."));

            var result = await _categoryService.UpdateAsync(dto);
            return result.Ok ? Ok(result) : BadRequest(result);
        }
        
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("Delete-Category")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _categoryService.DeleteAsync(id);
            return result.Ok ? Ok(result) : BadRequest(result);
        }
    }
}
