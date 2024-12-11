using SecureStore1.API.Data.Entities;
using SecureStore1.API.DTOs;

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
