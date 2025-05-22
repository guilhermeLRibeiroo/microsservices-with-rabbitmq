using AutoMapper;
using ItemService.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RestaurantService.AsyncDataServices;
using RestaurantService.DTO;
using RestaurantService.Models;

namespace RestaurantService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBusClient;

        public RestaurantController(IRestaurantRepository restaurantRepository,
            IMapper mapper,
            IMessageBusClient messageBusClient)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantReadDTO>> GetAll()
        {
            var restaurants = _restaurantRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<RestaurantReadDTO>>(restaurants));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantReadDTO>> GetRestaurantByID(Guid id)
        {
            var restaurante = await _restaurantRepository.GetById(id);
            if (restaurante != null)
            {
                return Ok(_mapper.Map<RestaurantReadDTO>(restaurante));
            }

            return NotFound();
        }

        [HttpPost]
        [ActionName(nameof(Create))]
        public async Task<ActionResult<RestaurantReadDTO>> Create(CreateRestaurantDTO newRestaurant)
        {
            var restaurant = _mapper.Map<Restaurant>(newRestaurant);
            await _restaurantRepository.Create(restaurant);

            var restaurantDTO = _mapper.Map<RestaurantReadDTO>(restaurant);
            await _messageBusClient.PublishRestaurant(restaurantDTO);

            return CreatedAtRoute(new { restaurantDTO.Id }, restaurantDTO);
        }
    }
}
