using AutoMapper;
using SecureStore1.API.Data.Entities;
using SecureStore1.API.DTOs;
using SecureStore1.API.Repositories.Interfaces;
using SecureStore1.API.Services.Interfaces;

namespace SecureStore1.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public Task DeleteProductAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();

        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _productRepository.GetByIdAsync(productId);
        }

        public Task UpdateProductAsync(Product roduct)
        {
            throw new NotImplementedException();
        }
    }
}
