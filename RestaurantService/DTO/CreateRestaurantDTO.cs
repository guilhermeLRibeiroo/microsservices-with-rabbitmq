using System.ComponentModel.DataAnnotations;

namespace RestaurantService.DTO
{
    public class CreateRestaurantDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string WebSite { get; set; }
    }
}
