using SecureStore1.API.Data.Entities;
using SecureStore1.API.Models;

namespace SecureStore1.API.Services.Interfaces
{
    public interface ICartService
    {
        Task<ServiceResponse<Cart>> GetCartAsync(int userId);
        Task<ServiceResponse<string>> AddToCartAsync(int userId, int productId, int quantity);
        Task<ServiceResponse<string>> ClearCartAsync(int userId);
    }
}
