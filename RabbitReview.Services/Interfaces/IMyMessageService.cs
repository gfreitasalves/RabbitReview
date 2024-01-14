using RabbitReview.Models;

namespace RabbitReview.Services.Interfaces
{
    public interface IMyMessageService
    {
        Task SendMessage(MyMessage message);
        Task SendMessage(MyMessage message, string exchange);
    }
}