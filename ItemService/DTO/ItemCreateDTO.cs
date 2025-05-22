using System.ComponentModel.DataAnnotations;

namespace ItemService.DTO
{
    public class ItemCreateDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
