using SecureStore1.API.Data.Entities;
using SecureStore1.API.Enums;

namespace SecureStore1.API.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
        Task UpdateOrderStatusAsync(int orderId, OrderStatus status);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task DeleteOrderAsync(int orderId);
    }
}
