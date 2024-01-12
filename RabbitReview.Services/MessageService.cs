using RabbitReview.Models;
using RabbitReview.Repositories;

namespace RabbitReview.Services
{
    public interface IMessageService
    {
        Task SendMessage(Message message);
    }

    public class MessageService : IMessageService
    {
        public IMessageRepository _messageRepository { get; set; }

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }        

        public async Task SendMessage(Message message)
        {
            await _messageRepository.SendMessage(message);
        }
    }
}