using Microsoft.EntityFrameworkCore;
using RestaurantService.Models;

namespace ItemService.Data
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options) {}

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
