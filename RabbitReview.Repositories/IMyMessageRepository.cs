using RabbitReview.Models;

namespace RabbitReview.Repositories
{
    public interface IMyMessageRepository
    {
        Task PublishMessage(MyMessage message);
    }
}