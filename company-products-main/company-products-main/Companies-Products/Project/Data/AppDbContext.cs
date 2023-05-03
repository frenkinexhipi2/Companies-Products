using Microsoft.EntityFrameworkCore;
using Project.Data.Models;

namespace Project.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}