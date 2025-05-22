using AutoMapper;
using ItemService.Data.Interfaces;
using ItemService.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ItemService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public RestaurantController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantReadDTO>> GetAll()
        {
            var restaurants = _restaurantRepository.GetAll();

            if(restaurants == null || !restaurants.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<RestaurantReadDTO>>(restaurants));
        }
    }
}
