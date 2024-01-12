using RabbitReview.Models;

namespace RabbitReview.Repositories
{
    public interface IMyMessageRepository
    {
        Task SendMessage(MyMessage message);
    }

    public class MyMessageRepository : IMyMessageRepository
    {
        public IRabbitMQConnection _rabbitMQConnection { get; set; }

        public MyMessageRepository(IRabbitMQConnection rabbitMQConnection)
        {
                _rabbitMQConnection = rabbitMQConnection;
        }

        public async Task SendMessage(MyMessage message)
        {
            await _rabbitMQConnection.PublishMessage(message, "rabbitMessageQueue");           
        }
    }
}