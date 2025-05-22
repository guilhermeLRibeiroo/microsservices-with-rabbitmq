namespace ItemService.DTO
{
    public class RestaurantReadDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string WebSite { get; set; } = string.Empty;
    }
}
