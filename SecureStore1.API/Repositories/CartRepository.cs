using Microsoft.EntityFrameworkCore;
using SecureStore1.API.Data;
using SecureStore1.API.Data.Entities;
using SecureStore1.API.Repositories.Interfaces;

namespace SecureStore1.API.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly MyDbContext _context;
        public CartRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task AddItemToCartAsync(int userId, int productId, int quantity)
        {
            var cart = await GetCartByUserIdAsync(userId) ?? new Cart { UserId = userId, CartItems = new List<CartItem>() };

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }

            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
        }

        public async Task ClearCartAsync(int userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                await _context.SaveChangesAsync();
            }
        }
    }
}
