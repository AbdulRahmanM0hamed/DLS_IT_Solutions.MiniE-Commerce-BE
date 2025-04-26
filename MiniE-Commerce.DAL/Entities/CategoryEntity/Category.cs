using MiniE_Commerce.DAL.Entities.BaseEntities;
using MiniE_Commerce.DAL.Entities.ProductEntity;
using System.ComponentModel.DataAnnotations;

namespace MiniE_Commerce.DAL.Entities.CategoryEntity
{
    public class Category : BaseEntity
    {
        [Required, MaxLength(100)] public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }      

}
