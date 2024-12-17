using SecureStore1.API.Data.Entities;
using SecureStore1.API.Models;
using SecureStore1.API.Repositories.Interfaces;
using SecureStore1.API.Services.Interfaces;

namespace SecureStore1.API.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<ServiceResponse<Cart>> GetCartAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                return ServiceResponse<Cart>.FailureResponse("Cart not found");
            }

            return ServiceResponse<Cart>.SuccessResponse(cart);
        }

        public async Task<ServiceResponse<string>> AddToCartAsync(int userId, int productId, int quantity)
        {
            try
            {
                await _cartRepository.AddItemToCartAsync(userId, productId, quantity);
                return ServiceResponse<string>.SuccessResponse(null, "Item added to cart successfully");
            }
            catch (Exception ex)
            {
                return ServiceResponse<string>.FailureResponse(ex.Message);
            }
        }

        public async Task<ServiceResponse<string>> ClearCartAsync(int userId)
        {
            try
            {
                await _cartRepository.ClearCartAsync(userId);
                return ServiceResponse<string>.SuccessResponse(null, "Cart cleared successfully");
            }
            catch (Exception ex)
            {
                return ServiceResponse<string>.FailureResponse(ex.Message);
            }
        }
    }
}
