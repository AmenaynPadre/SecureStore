using AutoMapper;
using SecureStore1.API.Data.Entities;
using SecureStore1.API.DTOs;

namespace SecureStore1.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterUserDto>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<Cart, CartDto>();
            CreateMap<CartDto, Cart>();
            CreateMap<CartItem, CartItemDto>();
            CreateMap<CartItemDto, CartItem>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
