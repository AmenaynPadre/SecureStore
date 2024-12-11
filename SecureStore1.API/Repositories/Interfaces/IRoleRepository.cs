using SecureStore1.API.Data.Entities;

namespace SecureStore1.API.Repositories.Interfaces
{
    public interface IRoleRepository 
    {
        Task<Role> FindByNameAsync(string roleName);
        Task AddAsync(Role role);
    }
}
