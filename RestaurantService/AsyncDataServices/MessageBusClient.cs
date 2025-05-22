using RabbitMQ.Client;
using RestaurantService.DTO;
using System.Text;
using System.Text.Json;

namespace RestaurantService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly RabbitMQConfiguration _rabbitmqConfiguration;

        public MessageBusClient(RabbitMQConfiguration rabbitmqConfiguration)
        {
            _rabbitmqConfiguration = rabbitmqConfiguration;
        }

        public async Task PublishRestaurant(RestaurantReadDTO newRestaurant)
        {
            var factory = new ConnectionFactory { HostName = _rabbitmqConfiguration.HostName, UserName = _rabbitmqConfiguration.UserName, Password = _rabbitmqConfiguration.Password };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(exchange: "create-restaurant", type: ExchangeType.Fanout);

            var message = JsonSerializer.Serialize(newRestaurant);
            var body = Encoding.UTF8.GetBytes(message);
            await channel.BasicPublishAsync(exchange: "create-restaurant", routingKey: string.Empty, body: body);
        }
    }
}
