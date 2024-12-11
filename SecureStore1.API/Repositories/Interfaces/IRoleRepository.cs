using SecureStore1.API.Models;

namespace SecureStore1.API.Repositories.Interfaces
{
    public interface IRoleRepository 
    {
        Task<Role> FindByNameAsync(string roleName);
        Task AddAsync(Role role);
    }
}
