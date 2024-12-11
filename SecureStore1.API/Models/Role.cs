using Microsoft.AspNetCore.Identity;

namespace SecureStore1.API.Models
{
    public class Role 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
