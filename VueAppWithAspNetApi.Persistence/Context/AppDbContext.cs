using Microsoft.EntityFrameworkCore;
using VueAppWithAspNetApi.Core.Models;

namespace VueAppWithAspNetApi.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
} 