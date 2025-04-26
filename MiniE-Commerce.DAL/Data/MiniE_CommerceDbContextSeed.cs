using MiniE_Commerce.DAL.Entities.CategoryEntity;
using MiniE_Commerce.DAL.Entities.ProductEntity;

namespace MiniE_Commerce.DAL.Data
{
    public class MiniE_CommerceDbContextSeed
    {
        public static async Task SeedAsync(MiniE_CommerceDbContext _dbContext)
        {
            if (!_dbContext.Categories.Any())
            {
                var categories = new List<Category>();
                var products = new List<Product>();

                for (int i = 1; i <= 10; i++)
                {
                    var categoryId = Guid.NewGuid();

                    categories.Add(new Category
                    {
                        Id = categoryId,
                        Name = $"Category {i}",

                    });

                    for (int j = 1; j <= 10; j++)
                    {
                        products.Add(new Product
                        {
                            Id = Guid.NewGuid(),
                            Name = $"Product {j} of Category {i}",
                            Description = $"Description for Product {j} in Category {i}",
                            Price = 10 * j,
                            Stock = 100 + j,
                            CategoryId = categoryId,
                        });
                    }
                }
              
                await _dbContext.Categories.AddRangeAsync(categories);
                await _dbContext.Products.AddRangeAsync(products);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
