using SecureStore1.API.Models;

namespace SecureStore1.API.Services.Interfaces
{
    public interface ICartService
    {
        Task<Cart> GetCartAsync(int userId);
        Task AddToCartAsync(int userId, int productId, int quantity, decimal unitPrice);
        Task ClearCartAsync(int userId);
    }
}
