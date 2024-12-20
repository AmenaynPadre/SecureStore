using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureStore1.API.Data.Entities;
using SecureStore1.API.DTOs;
using SecureStore1.API.Models;
using SecureStore1.API.Services.Interfaces;

namespace SecureStore1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody]int userId)
        {
            var response = await _orderService.CreateOrderAsync(userId);

            if (response.Success)
            {
                return Ok(ApiResponse<OrderDto>.SuccessResponse(response.Data, "Order created successfully."));
            }

            return BadRequest(ApiResponse<OrderDto>.FailureResponse(response.Message));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var response = await _orderService.GetOrderByIdAsync(id);

            if (response.Success)
            {
                return Ok(ApiResponse<OrderDto>.SuccessResponse(response.Data));
            }

            return NotFound(ApiResponse<OrderDto>.FailureResponse(response.Message));
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            var response = await _orderService.GetOrdersByUserIdAsync(userId);

            if (response.Success)
            {
                return Ok(ApiResponse<IEnumerable<OrderDto>>.SuccessResponse(response.Data));
            }

            return NotFound(ApiResponse<IEnumerable<OrderDto>>.FailureResponse(response.Message));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var response = await _orderService.GetAllOrdersAsync();

            if (response.Success)
            {
                return Ok(ApiResponse<IEnumerable<OrderDto>>.SuccessResponse(response.Data));
            }

            return NoContent();
        }
    }
}
