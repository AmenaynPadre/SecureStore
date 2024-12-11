using SecureStore1.API.DTOs;
using SecureStore1.API.Enums;

namespace SecureStore1.API.Services.Interfaces
{
    public interface IOrderService
    {
        Task<int> CreateOrderAsync(int userId, IEnumerable<OrderItemDto> items);
        Task<OrderDto> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(int userId);
        Task UpdateOrderStatusAsync(int orderId, OrderStatus status);
        Task AddToCartAsync(int userId, OrderItemDto item);
        Task RemoveFromCartAsync(int userId, int itemId);
        Task<IEnumerable<OrderItemDto>> GetCartItemsAsync(int userId);
        Task ClearCartAsync(int userId);
    }
}
