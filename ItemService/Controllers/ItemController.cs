using AutoMapper;
using ItemService.Data.Interfaces;
using ItemService.DTO;
using ItemService.Models;
using Microsoft.AspNetCore.Mvc;

namespace ItemService.Controllers
{
    [Route("api/[controller]/{restaurantId}")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public ItemController(IItemRepository itemRepository, IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ItemReadDTO>> GetItemsByRestaurantID(Guid restaurantId)
        {
            var itens = _itemRepository.GetByRestaurantId(restaurantId);

            if (itens.Count() == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ItemReadDTO>>(itens));
        }

        [HttpGet("{itemId}")]
        public ActionResult<ItemReadDTO> GetItemByID(Guid itemId)
        {
            var item = _itemRepository.GetById(itemId);

            if (item == null) { return NotFound(); }

            return Ok(_mapper.Map<ItemReadDTO>(item));
        }

        [HttpPost]
        public ActionResult<ItemReadDTO> CreateItemLinkedToRestaurantID(Guid restaurantId, ItemCreateDTO item)
        {
            if (!_restaurantRepository.ThisIDExists(restaurantId))
            {
                return NotFound();
            }

            var newItem = _mapper.Map<Item>(item);
            newItem.RestaurantId = restaurantId;

            _itemRepository.Create(newItem);

            var response = _mapper.Map<ItemReadDTO>(item);

            return CreatedAtRoute(nameof(CreateItemLinkedToRestaurantID), new { restaurantId, ItemId = response.Id }, response);
        }
    }
}
