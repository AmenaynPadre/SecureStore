using SecureStore1.API.Data.Entities;
using SecureStore1.API.DTOs;
using SecureStore1.API.Enums;
using SecureStore1.API.Models;

namespace SecureStore1.API.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ServiceResponse<OrderDto>> CreateOrderAsync(int userId);
        Task<ServiceResponse<OrderDto>> GetOrderByIdAsync(int orderId);
        Task<ServiceResponse<IEnumerable<OrderDto>>> GetOrdersByUserIdAsync(int userId);
        Task<ServiceResponse<List<OrderDto>>> GetAllOrdersAsync();
    }
}
