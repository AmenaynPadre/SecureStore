namespace SecureStore1.API.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
