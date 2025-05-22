using AutoMapper;
using ItemService.Data.Interfaces;
using ItemService.DTO;
using ItemService.Models;
using System.Text.Json;

namespace ItemService.EventProcessing
{
    public class CreateRestaurantEvent : ICreateRestaurantEvent
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;

        public CreateRestaurantEvent(IMapper mapper, IServiceScopeFactory scopeFactory)
        {
            _mapper = mapper;
            _scopeFactory = scopeFactory;
        }

        public void Process(string message)
        {
            using var scope = _scopeFactory.CreateScope();
            var restaurantRepository = scope.ServiceProvider.GetRequiredService<IRestaurantRepository>();
            var newRestaurantDTO = JsonSerializer.Deserialize<NewRestaurantDTO>(message);
            var restaurant = _mapper.Map<Restaurant>(newRestaurantDTO);

            if(!restaurantRepository.ThisExternalRestaurantIdExists(restaurant.ExternalId))
            {
                restaurantRepository.Create(restaurant).Wait();
            }
        }
    }
}
