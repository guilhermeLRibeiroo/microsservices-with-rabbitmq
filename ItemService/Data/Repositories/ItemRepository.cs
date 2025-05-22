using ItemService.Data.Interfaces;
using ItemService.Models;

namespace ItemService.Data.Repositories
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(DefaultDbContext context) : base(context)
        {
        }

        public IEnumerable<Item> GetByRestaurantId(Guid restaurantId)
        {
            return this.GetAll().Where(i => i.RestaurantId == restaurantId);
        }
    }
}
