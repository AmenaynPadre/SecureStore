﻿using Microsoft.AspNetCore.Identity;

namespace SecureStore1.API.Data.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}