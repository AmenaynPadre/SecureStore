using Microsoft.EntityFrameworkCore;
using SecureStore1.API.Data;
using SecureStore1.API.Data.Entities;
using SecureStore1.API.Repositories.Interfaces;

namespace SecureStore1.API.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly MyDbContext _context;

        public RoleRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<Role> FindByNameAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }
        public async Task<User> GetByUserNameAsync(string userName)
        {
            return await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }
    }
}
