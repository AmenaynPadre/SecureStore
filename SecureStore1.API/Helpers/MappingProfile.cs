using AutoMapper;
using SecureStore1.API.DTOs;
using SecureStore1.API.Models;

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
        }
    }
}
