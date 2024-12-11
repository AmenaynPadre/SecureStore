using SecureStore1.API.Data.Entities;
using SecureStore1.API.Repositories.Interfaces;

namespace SecureStore1.API.Repositories
{
    public class CartRepository : ICartRepository
    {
        public Task AddItemToCartAsync(int userId, int productId, int quantity, decimal unitPrice)
        {
            throw new NotImplementedException();
        }

        public Task ClearCartAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> GetCartByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
