using AutoMapper;
using ItemService.DTO;
using ItemService.Models;

namespace ItemService.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Item, ItemReadDTO>();
            CreateMap<ItemCreateDTO, Item>();

            CreateMap<Restaurant, RestaurantReadDTO>();
            CreateMap<NewRestaurantDTO, Restaurant>()
                .ForMember(restaurant => restaurant.ExternalId, opt => opt.MapFrom(newRestaurant => newRestaurant.Id));
        }
    }
}
