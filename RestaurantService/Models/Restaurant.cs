using ItemService.Models;
using System.ComponentModel.DataAnnotations;

namespace RestaurantService.Models
{
    public class Restaurant : BaseModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string WebSite { get; set; } = string.Empty;
    }
}
