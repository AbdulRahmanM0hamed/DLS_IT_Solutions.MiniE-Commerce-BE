using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniE_Commerce.BLL.Helper.SeedingData;
using MiniE_Commerce.DAL.Data;
using MiniE_Commerce.DAL.Entities.User;
using MiniE_Commerce.Extension;
using MiniE_Commerce.Middlewares;

namespace MiniE_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddMemoryCache();

            builder.Services.AddControllers();

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            // Database Context
            builder.Services.AddDbContext<MiniE_CommerceDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddApplicationServices();

            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerDocumentation();

            var app = builder.Build();

            #region Database migration and seeding
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = services.GetRequiredService<MiniE_CommerceDbContext>();
                await dbContext.Database.MigrateAsync();

                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                var seeder = new RoleSeeder(roleManager, userManager);
                await seeder.SeedRolesAndAdminAsync();

                await MiniE_CommerceDbContextSeed.SeedAsync(dbContext);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during migration.");
            }
            #endregion

            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseForwardedHeaders();

            app.UseHttpsRedirection();

            app.UseCors(options =>
            {
                options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
            });

            app.UseStaticFiles();

            app.UseRequestLocalization();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
