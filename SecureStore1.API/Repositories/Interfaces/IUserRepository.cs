using SecureStore1.API.Models;

namespace SecureStore1.API.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByUserNameAsync(string userName);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
