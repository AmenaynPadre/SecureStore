using SecureStore1.API.Data.Entities;
using SecureStore1.API.DTOs;
using SecureStore1.API.Models;

namespace SecureStore1.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<string>> RegisterAsync(RegisterUserDto userDto);
        Task<ServiceResponse<string>> LoginAsync(LoginUserDto loginDto);
        Task<ServiceResponse<UserDto>> GetUserByIdAsync(int userId);
        Task<ServiceResponse<IEnumerable<UserDto>>> GetAllUsersAsync();
        Task<ServiceResponse<string>> UpdateUserAsync(UserDto userDto);
        Task<ServiceResponse<string>> DeleteUserAsync(int userId);
    }
}
