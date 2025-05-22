namespace ItemService.DTO
{
    public class ItemReadDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public int RestaurantId { get; set; }
    }
}
