using SecureStore1.API.Models;

namespace SecureStore1.API.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserIdAsync(int userId);
        Task AddItemToCartAsync(int userId, int productId, int quantity, decimal unitPrice);
        Task ClearCartAsync(int userId);
    }
}
