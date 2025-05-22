using ItemService.Data;
using ItemService.Data.Interfaces;
using RestaurantService.Models;

namespace RestaurantService.Data.Repositories
{
    public class RestaurantRepository : BaseRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(DefaultDbContext context) : base(context)
        {
        }
    }
}
