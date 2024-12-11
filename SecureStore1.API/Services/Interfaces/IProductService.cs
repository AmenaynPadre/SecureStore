using SecureStore1.API.DTOs;
using SecureStore1.API.Models;

namespace SecureStore1.API.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int productId);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task UpdateProductAsync(Product roduct);
        Task DeleteProductAsync(int productId);
    }
}
