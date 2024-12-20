using SecureStore1.API.Data.Entities;
using SecureStore1.API.DTOs;
using SecureStore1.API.DTOs.ProductDto;
using SecureStore1.API.Models;

namespace SecureStore1.API.Services.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse<ProductDto>> GetProductByIdAsync(int productId);
        Task<ServiceResponse<IEnumerable<ProductDto>>> GetAllProductsAsync();
        Task<ServiceResponse<string>> UpdateProductAsync(ProductDto product);
        Task<ServiceResponse<string>> DeleteProductAsync(int productId);
        Task<ServiceResponse<string>> CreateProductAsync(ProductCreateDto product);

    }
}
