using Microsoft.AspNetCore.Mvc;
using MiniE_Commerce.BLL.Helper.Mapper;
using MiniE_Commerce.BLL.Interfaces;
using MiniE_Commerce.BLL.Repositories;
using MiniE_Commerce.BLL.Services.Auth;
using MiniE_Commerce.BLL.Services.Auth.Interface;
using MiniE_Commerce.BLL.Services.CartItemServices;
using MiniE_Commerce.BLL.Services.CartItemServices.Interface;
using MiniE_Commerce.BLL.Services.CategoryServices;
using MiniE_Commerce.BLL.Services.CategoryServices.Interface;
using MiniE_Commerce.BLL.Services.ProductServices;
using MiniE_Commerce.BLL.Services.ProductServices.Interface;
using MiniE_Commerce.Errors;

namespace MiniE_Commerce.Extension
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MappingProfile));

            //controller Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductService, ProductServices>();
            services.AddScoped<ICategoryServices, CategoryService>();    
            services.AddScoped<ICartItemServices, CartItemService>();


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(x => x.Value!.Errors.Count > 0)
                    .SelectMany(x => x.Value!.Errors)
                    .Select(e => e.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors,
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }
    }
}
