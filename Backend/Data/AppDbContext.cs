using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        //User
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
