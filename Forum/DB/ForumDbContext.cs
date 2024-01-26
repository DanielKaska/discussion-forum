using Microsoft.EntityFrameworkCore;
using Forum.DB.Entities;

namespace Forum.DB
{
    public class ForumDbContext : DbContext 
    {
        private string connectionString = "Server=localhost;Database=ForumDB;Integrated Security=True;TrustServerCertificate=True;";

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<User>().Property(u => u.Id).IsRequired();
            mb.Entity<User>().Property(u => u.Email).IsRequired();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
