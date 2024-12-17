using AutoMapper;
using SecureStore1.API.Data.Entities;
using SecureStore1.API.DTOs;
using SecureStore1.API.Models;
using SecureStore1.API.Repositories.Interfaces;

namespace SecureStore1.API.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<OrderDto>> CreateOrderAsync(Order order)
        {
            try
            {
                var createdOrder = await _orderRepository.CreateOrderAsync(order);
                var dto = _mapper.Map<OrderDto>(createdOrder);
                return ServiceResponse<OrderDto>.SuccessResponse(dto, "Order created successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResponse<OrderDto>.FailureResponse(ex.Message);
            }
        }

        public async Task<ServiceResponse<Order>> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return ServiceResponse<Order>.FailureResponse("Order not found.");
            }

            return ServiceResponse<Order>.SuccessResponse(order);
        }

        public async Task<ServiceResponse<IEnumerable<Order>>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return ServiceResponse<IEnumerable<Order>>.SuccessResponse(orders);
        }
    }
}
