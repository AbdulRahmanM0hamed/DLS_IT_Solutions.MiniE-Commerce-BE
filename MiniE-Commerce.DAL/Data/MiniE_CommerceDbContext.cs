using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniE_Commerce.DAL.Entities.CategoryEntity;
using MiniE_Commerce.DAL.Entities.ProductEntity;
using MiniE_Commerce.DAL.Entities.User;
using System.Reflection;

namespace MiniE_Commerce.DAL.Data
{
   public class MiniE_CommerceDbContext : IdentityDbContext<ApplicationUser>
    {
        public MiniE_CommerceDbContext(DbContextOptions<MiniE_CommerceDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }       

    }
}
