using RestaurantService.DTO;

namespace RestaurantService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        Task PublishRestaurant(RestaurantReadDTO newRestaurant);
    }
}
