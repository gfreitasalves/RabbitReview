using RabbitReview.Models;

namespace RabbitReview.Repositories.Interfaces
{
    public interface IMyMessageRepository
    {
        Task PublishMessage(MyMessage message);
        Task PublishMessage(MyMessage message, string exchange);
        Task SubscribeMessages(Action<byte[]> queueItemReadAction);
        Task SubscribeMessages(Action<byte[]> queueItemReadAction, string exchange);
    }
}