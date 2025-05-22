using System.ComponentModel.DataAnnotations;

namespace ItemService.Models
{
    public abstract class BaseModel
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
