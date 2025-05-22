using AutoMapper;
using RestaurantService.DTO;
using RestaurantService.Models;

namespace RestaurantService.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Restaurant, RestaurantReadDTO>();
            CreateMap<CreateRestaurantDTO, Restaurant>();
        }
    }
}
