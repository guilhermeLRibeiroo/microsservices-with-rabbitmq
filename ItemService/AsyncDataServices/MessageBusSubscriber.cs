
using ItemService.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ItemService.AsyncDataServices
{
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly ICreateRestaurantEvent _eventHandler;
        private readonly RabbitMQConfiguration _configuration;

        public MessageBusSubscriber(ICreateRestaurantEvent createRestaurantEvent, RabbitMQConfiguration configuration)
        {
            _eventHandler = createRestaurantEvent;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory { HostName = _configuration.HostName, UserName = _configuration.UserName, Password = _configuration.Password };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(exchange: "create-restaurant", type: ExchangeType.Fanout);

            var queue = await channel.QueueDeclareAsync();
            await channel.QueueBindAsync(queue: queue.QueueName, exchange: "create-restaurant", routingKey: string.Empty);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += (moduleHandle, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());

                _eventHandler.Process(message);
                return Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(queue: queue.QueueName, autoAck: true, consumer: consumer);
        }
    }
}
