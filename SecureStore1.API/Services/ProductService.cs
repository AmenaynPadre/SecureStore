using AutoMapper;
using SecureStore1.API.Data.Entities;
using SecureStore1.API.DTOs;
using SecureStore1.API.DTOs.ProductDto;
using SecureStore1.API.Models;
using SecureStore1.API.Repositories;
using SecureStore1.API.Repositories.Interfaces;
using SecureStore1.API.Services.Interfaces;

namespace SecureStore1.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<string>> CreateProductAsync(ProductCreateDto product)
        {
            var productEntity = _mapper.Map<Product>(product);
            await _productRepository.AddAsync(productEntity);
            return ServiceResponse<string>.SuccessResponse("Product created successfully.");

        }

        public async Task<ServiceResponse<string>> DeleteProductAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return ServiceResponse<string>.FailureResponse("Product not found.");
            }

            await _productRepository.DeleteAsync(product.Id);
            return ServiceResponse<string>.SuccessResponse("Product deleted successfully.");
        }

        public async Task<ServiceResponse<IEnumerable<ProductDto>>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return ServiceResponse<IEnumerable<ProductDto>>.SuccessResponse(productDtos);
        }

        public async Task<ServiceResponse<ProductDto>> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return ServiceResponse<ProductDto>.FailureResponse("Product not found.");
            }

            var productDto = _mapper.Map<ProductDto>(product);
            return ServiceResponse<ProductDto>.SuccessResponse(productDto);
        }

        public async Task<ServiceResponse<string>> UpdateProductAsync(ProductDto product)
        {
            var existingProduct = await _productRepository.GetByIdAsync(product.Id);
            if (existingProduct == null)
            {
                return ServiceResponse<string>.FailureResponse("Product not found.");
            }

            var productDto =  _mapper.Map(product, existingProduct);
            await _productRepository.UpdateAsync(productDto);
            return ServiceResponse<string>.SuccessResponse("Product updated successfully.");
        }
    }
}
