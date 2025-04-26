using MiniE_Commerce.DAL.Entities.BaseEntities;
using MiniE_Commerce.DAL.Entities.CategoryEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniE_Commerce.DAL.Entities.ProductEntity
{
    public class Product : BaseEntity
    {     
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
    }
}
