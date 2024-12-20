using AutoMapper;
using SecureStore1.API.Data.Entities;
using SecureStore1.API.DTOs;
using SecureStore1.API.Models;
using SecureStore1.API.Repositories;
using SecureStore1.API.Repositories.Interfaces;
using SecureStore1.API.Services.Interfaces;

namespace SecureStore1.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository, 
            IProductRepository productRepository,
            IMapper mapper,
            ICartRepository cartRepository
            )
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _cartRepository = cartRepository;
        }

        public async Task<ServiceResponse<OrderDto>> CreateOrderAsync(int userId)
        {
            try
            {
                var cart = await _cartRepository.GetCartByUserIdAsync(userId);
                if (cart == null || !cart.CartItems.Any())
                {
                    return ServiceResponse<OrderDto>.FailureResponse("Cart is empty or not found.");
                }

                var order = new Order
                {
                    UserId = userId,
                    OrderItems = cart.CartItems.Select(item => new OrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Product.Price 
                    }).ToList()
                };

                foreach (var orderItem in order.OrderItems)
                {
                    var product = await _productRepository.GetByIdAsync(orderItem.ProductId);
                    if (product == null || product.Stock < orderItem.Quantity)
                    {
                        return ServiceResponse<OrderDto>.FailureResponse($"Not enough stock for product {product.Name}.");
                    }

                    product.Stock -= orderItem.Quantity;
                    await _productRepository.UpdateAsync(product);
                }

                await _orderRepository.CreateOrderAsync(order);

                await _cartRepository.ClearCartAsync(userId);

                var orderDto = _mapper.Map<OrderDto>(order);

                return ServiceResponse<OrderDto>.SuccessResponse(orderDto, "Order created successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResponse<OrderDto>.FailureResponse(ex.Message);
            }
        }
        

        public async Task<ServiceResponse<OrderDto>> GetOrderByIdAsync(int orderId)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(orderId);
                if (order == null)
                {
                    return ServiceResponse<OrderDto>.FailureResponse($"Order with ID {orderId} not found.");
                }

                var orderDto = _mapper.Map<OrderDto>(order);

                return ServiceResponse<OrderDto>.SuccessResponse(orderDto, "Order retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResponse<OrderDto>.FailureResponse(ex.Message);
            }
        }

        public async Task<ServiceResponse<IEnumerable<OrderDto>>> GetOrdersByUserIdAsync(int userId)
        {
            try
            {
                var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);

                if (orders == null || !orders.Any())
                {
                    return ServiceResponse<IEnumerable<OrderDto>>.FailureResponse($"No orders found for user ID {userId}.");
                }

                var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

                return ServiceResponse<IEnumerable<OrderDto>>.SuccessResponse(orderDtos, "Orders retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<OrderDto>>.FailureResponse(ex.Message);
            }
        }

        public async Task<ServiceResponse<List<OrderDto>>> GetAllOrdersAsync()
        {
            try
            {
                var orders = await _orderRepository.GetAllOrdersAsync();

                if (orders == null || !orders.Any())
                {
                    return ServiceResponse<List<OrderDto>>.FailureResponse("No orders found.");
                }

                var orderDtos = _mapper.Map<List<OrderDto>>(orders);

                return ServiceResponse<List<OrderDto>>.SuccessResponse(orderDtos, "All orders retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<OrderDto>>.FailureResponse(ex.Message);
            }
        }
    }
}
