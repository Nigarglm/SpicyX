using Microsoft.EntityFrameworkCore;
using SpicyX.Models;

namespace SpicyX.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Position> Positions { get; set; }
    }
}
