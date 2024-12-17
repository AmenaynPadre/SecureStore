using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureStore1.API.Data.Entities;
using SecureStore1.API.Models;
using SecureStore1.API.Services.Interfaces;

namespace SecureStore1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<ApiResponse<Cart>>> GetCart(int userId)
        {
            var serviceResponse = await _cartService.GetCartAsync(userId);
            if (!serviceResponse.Success)
            {
                return NotFound(ApiResponse<Cart>.FailureResponse(serviceResponse.Message));
            }

            return Ok(ApiResponse<Cart>.SuccessResponse(serviceResponse.Data, serviceResponse.Message));
        }

        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<string>>> AddToCart(int userId, int productId, int quantity)
        {
            var serviceResponse = await _cartService.AddToCartAsync(userId, productId, quantity);
            if (!serviceResponse.Success)
            {
                return BadRequest(ApiResponse<string>.FailureResponse(serviceResponse.Message));
            }

            return Ok(ApiResponse<string>.SuccessResponse(serviceResponse.Data, serviceResponse.Message));
        }

        [HttpDelete("{userId}/clear")]
        public async Task<ActionResult<ApiResponse<string>>> ClearCart(int userId)
        {
            var serviceResponse = await _cartService.ClearCartAsync(userId);
            if (!serviceResponse.Success)
            {
                return StatusCode(500, ApiResponse<string>.FailureResponse(serviceResponse.Message));
            }

            return Ok(ApiResponse<string>.SuccessResponse(serviceResponse.Data, serviceResponse.Message));
        }
    }
}
