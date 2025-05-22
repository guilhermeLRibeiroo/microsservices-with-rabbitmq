using System.ComponentModel.DataAnnotations;

namespace ItemService.Models
{
    public class Item : BaseModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public double Price { get; set; }

        [Required]
        public Guid RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }
    }
}
