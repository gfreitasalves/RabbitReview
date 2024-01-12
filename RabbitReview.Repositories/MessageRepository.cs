using RabbitReview.Models;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client;

namespace RabbitReview.Repositories
{
    public interface IMessageRepository
    {
        Task SendMessage(Message message);
    }

    public class MessageRepository : IMessageRepository
    {
        public Task SendMessage(Message message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "123456"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "rabbitMessageQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "",
                                     routingKey: "rabbitMessageQueue",
                                     basicProperties: null,
                                     body: body);
            }

            return Task.CompletedTask;
        }
    }
}