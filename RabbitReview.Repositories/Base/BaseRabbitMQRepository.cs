using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace RabbitReview.Repositories
{
    public abstract class BaseRabbitMQRepository<T> : IDisposable
    {

        private readonly ConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queue;


        public BaseRabbitMQRepository(string queue)
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "123456"
            };

            _queue = queue;
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _channel?.Dispose();
        }

        public async Task PublishMessage(T message)
        {
            await PublishMessage(message, string.Empty);
        }

        public Task PublishMessage(T message, string exchange)
        {
            string json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(exchange: exchange,
                                 routingKey: _queue,
                                 basicProperties: null,
                                 body: body);

            return Task.CompletedTask;
        }

        public async Task SubscribeMessages(Action<byte[]> queueItemReadAction)
        {
            await SubscribeMessages(queueItemReadAction, string.Empty);
        }

        public Task SubscribeMessages(Action<byte[]> queueItemReadAction, string exchange)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                queueItemReadAction(ea.Body.ToArray());

            };
            _channel.BasicConsume(queue: _queue,
                                 autoAck: true,
                                 consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
