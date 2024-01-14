using RabbitReview.Models;
using RabbitReview.Repositories.Interfaces;

namespace RabbitReview.Repositories
{

    public class MyMessageRepository : BaseRabbitMQRepository<MyMessage>, IMyMessageRepository
    {
        public MyMessageRepository():base("rabbitMessageQueue")
        {
        }
    }
}