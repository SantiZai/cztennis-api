using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Context
{
    public class CZContext : DbContext
    {
        public CZContext(DbContextOptions<CZContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Strung> Strungs { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
