﻿using System.Diagnostics.CodeAnalysis;

namespace Forum.DB.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<Post> Posts { get; set; }

        public virtual Role Role { get; set; }
        public int RoleId { get; set; }
    }
}
