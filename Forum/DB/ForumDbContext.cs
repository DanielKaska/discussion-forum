using Microsoft.EntityFrameworkCore;
using Forum.DB.Entities;

namespace Forum.DB
{
    public class ForumDbContext : DbContext 
    {
        private readonly string connectionString = "Data Source=DANIEL;Initial Catalog=ForumDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<User>().Property(u => u.Id).IsRequired();
            mb.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(40);
            mb.Entity<User>().Property(u => u.Password).IsRequired();
            mb.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(20);
            mb.Entity<User>().Property(u => u.RoleId).HasDefaultValue(1);
                

            mb.Entity<Role>().HasData(new Role() { Id = 1, Name = "User", Description = "Normal user, can create posts and comment"});
            mb.Entity<Role>().HasData(new Role() { Id = 2, Name = "Moderator", Description = "Moderator, can mute or ban users, and delete their posts" });
            mb.Entity<Role>().HasData(new Role() { Id = 3, Name = "Admin", Description = "Admin, has right to do everything" });
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
