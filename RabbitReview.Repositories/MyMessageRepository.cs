using RabbitReview.Models;

namespace RabbitReview.Repositories
{

    public class MyMessageRepository : BaseRabbitMQRepository<MyMessage>, IMyMessageRepository
    {
        public MyMessageRepository():base("rabbitMessageQueue")
        {
        }
    }
}