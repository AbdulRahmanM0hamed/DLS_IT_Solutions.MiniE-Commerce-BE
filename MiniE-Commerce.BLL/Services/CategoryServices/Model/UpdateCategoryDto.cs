using System.ComponentModel.DataAnnotations;

namespace MiniE_Commerce.BLL.Services.CategoryServices.Model
{
    public class UpdateCategoryDto
    {
        [Required(ErrorMessage = "Category ID is Required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Category Name is Required")]
        public string Name { get; set; }
    }
}
