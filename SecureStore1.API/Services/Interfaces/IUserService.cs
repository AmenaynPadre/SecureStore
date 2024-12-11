using SecureStore1.API.DTOs;
using SecureStore1.API.Models;

namespace SecureStore1.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterUserDto userDto);
        Task<string> LoginAsync(LoginUserDto loginDto);
        Task<UserDto> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task UpdateUserAsync(UserDto userDto);
        Task DeleteUserAsync(int userId);
    }
}
