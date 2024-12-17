using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureStore1.API.Data.Entities;
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

        //[HttpPost]
        //public async Task<ActionResult<ApiResponse<Order>>> CreateOrder([FromBody] Order order)
        //{
        //    var serviceResponse = await _orderService.CreateOrderAsync(order);
        //    if (!serviceResponse.Success)
        //    {
        //        return BadRequest(ApiResponse<Order>.FailureResponse(serviceResponse.Message));
        //    }

        //    return Ok(ApiResponse<Order>.SuccessResponse(serviceResponse.Data, serviceResponse.Message));
        //}

        //[HttpGet("{orderId}")]
        //public async Task<ActionResult<ApiResponse<Order>>> GetOrderById(int orderId)
        //{
        //    var serviceResponse = await _orderService.GetOrderByIdAsync(orderId);
        //    if (!serviceResponse.Success)
        //    {
        //        return NotFound(ApiResponse<Order>.FailureResponse(serviceResponse.Message));
        //    }

        //    return Ok(ApiResponse<Order>.SuccessResponse(serviceResponse.Data, serviceResponse.Message));
        //}

        //[HttpGet("user/{userId}")]
        //public async Task<ActionResult<ApiResponse<IEnumerable<Order>>>> GetOrdersByUserId(int userId)
        //{
        //    var serviceResponse = await _orderService.GetOrdersByUserIdAsync(userId);
        //    return Ok(ApiResponse<IEnumerable<Order>>.SuccessResponse(serviceResponse.Data, serviceResponse.Message));
        //}
    }
}
