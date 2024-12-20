using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureStore1.API.DTOs.ProductDto;
using SecureStore1.API.Models;
using SecureStore1.API.Services.Interfaces;

namespace SecureStore1.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productDto)
        {
            var serviceResponse = await _productService.CreateProductAsync(productDto);

            if (!serviceResponse.Success)
            {
                return BadRequest(ApiResponse<string>.FailureResponse(serviceResponse.Message));
            }

            return Ok(ApiResponse<string>.SuccessResponse(serviceResponse.Message));
        }

        [HttpDelete("{productId}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var serviceResponse = await _productService.DeleteProductAsync(productId);

            if (!serviceResponse.Success)
            {
                return NotFound(ApiResponse<string>.FailureResponse(serviceResponse.Message));
            }

            return Ok(ApiResponse<string>.SuccessResponse(serviceResponse.Message));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var serviceResponse = await _productService.GetAllProductsAsync();

            if (!serviceResponse.Success)
            {
                return BadRequest(ApiResponse<IEnumerable<ProductDto>>.FailureResponse(serviceResponse.Message));
            }

            return Ok(ApiResponse<IEnumerable<ProductDto>>.SuccessResponse(serviceResponse.Data));
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var serviceResponse = await _productService.GetProductByIdAsync(productId);

            if (!serviceResponse.Success)
            {
                return NotFound(ApiResponse<ProductDto>.FailureResponse(serviceResponse.Message));
            }

            return Ok(ApiResponse<ProductDto>.SuccessResponse(serviceResponse.Data));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto productDto)
        {
            var serviceResponse = await _productService.UpdateProductAsync(productDto);

            if (!serviceResponse.Success)
            {
                return NotFound(ApiResponse<string>.FailureResponse(serviceResponse.Message));
            }

            return Ok(ApiResponse<string>.SuccessResponse(serviceResponse.Message));
        }
    }
}
