using AutoMapper;
using MiniE_Commerce.BLL.EntitiesSpecifications.Base;
using MiniE_Commerce.BLL.EntitiesSpecifications.ProductSpecifications;
using MiniE_Commerce.BLL.Helper.Response;
using MiniE_Commerce.BLL.Interfaces;
using MiniE_Commerce.BLL.Services.ProductServices.Interface;
using MiniE_Commerce.BLL.Services.ProductServices.Models;
using MiniE_Commerce.DAL.Entities.ProductEntity;

namespace MiniE_Commerce.BLL.Services.ProductServices
{
    public class ProductServices : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _genericRepository;
        public ProductServices(IMapper mapper, IGenericRepository<Product> genericRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
        }

        public async Task<ResponseModel<IReadOnlyList<GetProductsDto>>> GetAllProductsAsync()
        {
            (var products, int Count) = _genericRepository.GetAllEntitiesAndCountWithSpec(new GetAllProductSpecifications());

            if (!products.Any())
                return ResponseModel<IReadOnlyList<GetProductsDto>>.Error("No products found.");
            var mappedDate = _mapper.Map<IReadOnlyList<GetProductsDto>>(products);

            return ResponseModel<IReadOnlyList<GetProductsDto>>.Success(mappedDate);
        }
        public async Task<ResponseModel<GetProductByIdDto>> GetProductByIdAsync(Guid id)
        {
            var product = await _genericRepository.GetEntityWithSpec(new GetProductByIdSpecifications(id));

            if (product == null)
                return ResponseModel<GetProductByIdDto>.Error("No product found.");

            var mappedDate = _mapper.Map<GetProductByIdDto>(product);
            return ResponseModel<GetProductByIdDto>.Success(mappedDate);
        }
        public async Task<ResponseModel<IReadOnlyList<GetProductsDto>>> GetProductsByCategoryIdAsync(Guid categoryId)
        {
            var spec = new GetProductsByCategoryIdSpecification(categoryId);
            var products = await _genericRepository.GetAllEntitiesWithSpec(spec);

            if (!products.Any())
                return ResponseModel<IReadOnlyList<GetProductsDto>>.Error("No products found for this category.");

            var mappedData = _mapper.Map<IReadOnlyList<GetProductsDto>>(products);
            return ResponseModel<IReadOnlyList<GetProductsDto>>.Success(mappedData);
        }
        public async Task<ResponseModel<string>> AddProductAsync(AddProductsDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            await _genericRepository.CreateEntityAsync(product);
            await _genericRepository.SaveChangesAsync();

            return ResponseModel<string>.Success("Product added successfully.");
        }
        public async Task<ResponseModel<string>> DeleteProductAsync(Guid id)
        {
            var product = await _genericRepository.GetEntityById(id);
            if (product == null)
                return ResponseModel<string>.Error("Product not found.");

            _genericRepository.DeleteEntityAsync(product);
            await _genericRepository.SaveChangesAsync();

            return ResponseModel<string>.Success("Product deleted successfully.");
        }
        public async Task<ResponseModel<string>> UpdateProductAsync(UpdateProductsDto productDto)
        {
            var product = await _genericRepository.GetEntityById(productDto.Id);
            if (product == null)
                return ResponseModel<string>.Error("Product not found.");

            _mapper.Map(productDto, product);
            await _genericRepository.UpdateEntityAsync(product);
            await _genericRepository.SaveChangesAsync();
            return ResponseModel<string>.Success("Product updated successfully.");
        }
    }
}