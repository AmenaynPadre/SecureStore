using Microsoft.EntityFrameworkCore;
using SecureStore1.API.Models;

namespace SecureStore1.API.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>()
                .HasData(
                new Role()
                {
                    Id = 1,
                    Name = "User"
                },
                new Role()
                {
                    Id = 2,
                    Name = "Admin"
                },
                new Role()
                {
                    Id = 3,
                    Name = "Customer"
                }
                );
        }

    }
}
