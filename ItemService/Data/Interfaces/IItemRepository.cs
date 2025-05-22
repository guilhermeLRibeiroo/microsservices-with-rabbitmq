using ItemService.Models;

namespace ItemService.Data.Interfaces
{
    public interface IItemRepository : IBaseRepository<Item>
    {
        IEnumerable<Item> GetByRestaurantId(Guid restaurantId);
    }
}
