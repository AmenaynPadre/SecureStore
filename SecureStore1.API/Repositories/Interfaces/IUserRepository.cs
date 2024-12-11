using SecureStore1.API.Data.Entities;

namespace SecureStore1.API.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByUserNameAsync(string userName);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
