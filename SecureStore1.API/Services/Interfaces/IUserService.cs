using SecureStore1.API.Data.Entities;
using SecureStore1.API.DTOs;
using SecureStore1.API.Models;

namespace SecureStore1.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<string>> RegisterAsync(RegisterUserDto userDto);
        Task<ServiceResponse<string>> LoginAsync(LoginUserDto loginDto);
        Task<UserDto> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task UpdateUserAsync(UserDto userDto);
        Task DeleteUserAsync(int userId);
    }
}
