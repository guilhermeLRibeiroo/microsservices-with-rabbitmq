using System.ComponentModel.DataAnnotations;

namespace ItemService.Models
{
    public class Restaurant : BaseModel
    {
        [Required]
        public Guid ExternalId { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;

        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
