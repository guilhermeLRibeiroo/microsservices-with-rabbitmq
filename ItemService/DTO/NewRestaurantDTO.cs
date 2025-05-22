namespace ItemService.DTO
{
    public class NewRestaurantDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EventInfo { get; set; } = string.Empty;
    }
}
