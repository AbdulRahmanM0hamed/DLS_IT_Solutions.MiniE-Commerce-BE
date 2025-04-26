namespace MiniE_Commerce.BLL.Services.CartItemServices.Model
{
    public class AddCartItemDto
    {
        public string UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
