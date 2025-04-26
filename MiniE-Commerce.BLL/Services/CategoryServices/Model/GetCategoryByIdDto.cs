namespace MiniE_Commerce.BLL.Services.CategoryServices.Model
{
    public class GetCategoryByIdDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<GetProductsByCategoryDto> Products { get; set; } = new List<GetProductsByCategoryDto>();
    }
}
