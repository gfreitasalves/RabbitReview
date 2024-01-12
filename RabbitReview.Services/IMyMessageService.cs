using RabbitReview.Models;

namespace RabbitReview.Services
{
    public interface IMyMessageService
    {
        Task SendMessage(MyMessage message);
    }
}