using RabbitReview.Models;
using RabbitReview.Repositories;

namespace RabbitReview.Services
{
    public interface IMyMessageService
    {
        Task SendMessage(MyMessage message);
    }

    public class MyMessageService : IMyMessageService
    {
        public IMyMessageRepository _messageRepository { get; set; }

        public MyMessageService(IMyMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }        

        public async Task SendMessage(MyMessage message)
        {
            await _messageRepository.SendMessage(message);
        }
    }
}