namespace SecureStore1.API.DTOs
{
    public class CartDto
    {
        public int UserId { get; set; }
        public List<CartItemDto> Items { get; set; }
    }
}
