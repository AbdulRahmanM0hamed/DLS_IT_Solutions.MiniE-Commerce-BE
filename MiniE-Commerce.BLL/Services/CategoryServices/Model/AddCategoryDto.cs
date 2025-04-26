using System.ComponentModel.DataAnnotations;

namespace MiniE_Commerce.BLL.Services.CategoryServices.Model
{
    public class AddCategoryDto
    {
        [Required(ErrorMessage = "Category Name is Required")]
        public string Name { get; set; }
    }
}
