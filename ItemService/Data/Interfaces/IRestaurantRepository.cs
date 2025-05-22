using ItemService.Models;

namespace ItemService.Data.Interfaces
{
    public interface IRestaurantRepository : IBaseRepository<Restaurant>
    {
        bool ThisExternalRestaurantIdExists(Guid externalRestaurantID);
        bool ThisIDExists(Guid restaurantID);
    }
}
