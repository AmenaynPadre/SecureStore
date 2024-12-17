using System.Text.Json.Serialization;

namespace SecureStore1.API.Data.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore]
        public Cart Cart { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
    }
}
