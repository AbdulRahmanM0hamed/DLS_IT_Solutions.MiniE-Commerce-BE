using AutoMapper;
using MiniE_Commerce.BLL.EntitiesSpecifications.Base;
using MiniE_Commerce.BLL.EntitiesSpecifications.CategorySpecifications;
using MiniE_Commerce.BLL.Helper.Response;
using MiniE_Commerce.BLL.Interfaces;
using MiniE_Commerce.BLL.Services.CategoryServices.Interface;
using MiniE_Commerce.BLL.Services.CategoryServices.Model;
using MiniE_Commerce.DAL.Entities.CategoryEntity;

namespace MiniE_Commerce.BLL.Services.CategoryServices
{
    public class CategoryService : ICategoryServices
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Category> _repository;

        public CategoryService(IMapper mapper, IGenericRepository<Category> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseModel<IReadOnlyList<GetCategoryDto>>> GetAllAsync(BaseSpecificationParams specParams)
        {
            (var categories, int count) = _repository.GetAllEntitiesAndCountWithSpec(new GetAllCategorySpecifications(specParams));

            if (!categories.Any())
                return ResponseModel<IReadOnlyList<GetCategoryDto>>.Error("No categories found.");

            var mapped = _mapper.Map<IReadOnlyList<GetCategoryDto>>(categories);
            return ResponseModel<IReadOnlyList<GetCategoryDto>>.Success(mapped, specParams.PageIndex, specParams.PageSize, count);
        }

        public async Task<ResponseModel<GetCategoryByIdDto>> GetByIdAsync(Guid id)
        {
            var category = await _repository.GetEntityWithSpec(new GetCategoryByIdSpecifications(id));
            if (category == null)
                return ResponseModel<GetCategoryByIdDto>.Error("Category not found.");

            return ResponseModel<GetCategoryByIdDto>.Success(_mapper.Map<GetCategoryByIdDto>(category));
        }

        public async Task<ResponseModel<string>> AddAsync(AddCategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _repository.CreateEntityAsync(category);
            await _repository.SaveChangesAsync();
            return ResponseModel<string>.Success("Category added successfully.");
        }

        public async Task<ResponseModel<string>> UpdateAsync(UpdateCategoryDto categoryDto)
        {
            var category = await _repository.GetEntityById(categoryDto.Id);
            if (category == null)
                return ResponseModel<string>.Error("Category not found.");

            _mapper.Map(categoryDto, category);
            await _repository.UpdateEntityAsync(category);
            await _repository.SaveChangesAsync();
            return ResponseModel<string>.Success("Category updated successfully.");
        }

        public async Task<ResponseModel<string>> DeleteAsync(Guid id)
        {
            var category = await _repository.GetEntityById(id);
            if (category == null)
                return ResponseModel<string>.Error("Category not found.");

            _repository.DeleteEntityAsync(category);
            await _repository.SaveChangesAsync();
            return ResponseModel<string>.Success("Category deleted successfully.");
        }
    }

}
