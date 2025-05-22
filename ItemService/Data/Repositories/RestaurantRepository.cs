using ItemService.Data.Interfaces;
using ItemService.Models;

namespace ItemService.Data.Repositories
{
    public class RestaurantRepository : BaseRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(DefaultDbContext context) : base(context)
        {
        }

        public bool ThisExternalRestaurantIdExists(Guid externalRestaurantID)
        {
            return _context.Restaurants.Any(r => r.ExternalId == externalRestaurantID);
        }

        public bool ThisIDExists(Guid restaurantID)
        {
            return this.GetById(restaurantID) != null;
        }
    }
}
