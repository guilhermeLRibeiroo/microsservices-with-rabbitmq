namespace ItemService.EventProcessing
{
    public interface ICreateRestaurantEvent
    {
        void Process(string message);
    }
}
