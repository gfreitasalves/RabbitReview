using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitReview.Repositories
{

    public interface IRabbitMQConnection
    {
        Task PublishMessage<T>(T message, string queue);
    }
    public class RabbitMQConnection : IRabbitMQConnection
    {

        private readonly ConnectionFactory _connectionFactory;
        
        public RabbitMQConnection()
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "123456"
            };
        }

        public Task PublishMessage<T>(T message, string queue)
        {
            string json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            Publish(body, queue);

            return Task.CompletedTask;
        }

        private void Publish(byte[] body, string queue)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.BasicPublish(exchange: "",
                                     routingKey: queue,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
