using AutoMapper;
using MiniE_Commerce.BLL.Services.CartItemServices.Model;
using MiniE_Commerce.BLL.Services.CategoryServices.Model;
using MiniE_Commerce.BLL.Services.ProductServices.Models;
using MiniE_Commerce.DAL.Entities.CartItemEntity;
using MiniE_Commerce.DAL.Entities.CategoryEntity;
using MiniE_Commerce.DAL.Entities.ProductEntity;

namespace MiniE_Commerce.BLL.Helper.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddProductsDto, Product>()
                  .ReverseMap();

            CreateMap<UpdateProductsDto, Product>()
                .ReverseMap();

            CreateMap<GetProductsDto, Product>()
              .ForPath(dest => dest.Category.Name, opt => opt.MapFrom(src => src.CategoryName))
                .ReverseMap();

            CreateMap<GetProductByIdDto, Product>()
                .ReverseMap();


            CreateMap<Category, GetCategoryDto>()
                .ReverseMap();

            CreateMap<Category, GetCategoryByIdDto>()
              .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            CreateMap<AddCategoryDto, Category>()
                .ReverseMap();

            CreateMap<UpdateCategoryDto, Category>()
                .ReverseMap();
            
            
            CreateMap<AddCartItemDto, CartItem>()
                .ReverseMap();
            
            CreateMap<UpdateCartItemDto, CartItem>()
                .ReverseMap();
            
            CreateMap<GetCartItemDto, CartItem>()
                .ReverseMap();



        }
    }
}
