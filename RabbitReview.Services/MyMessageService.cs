using RabbitReview.Models;
using RabbitReview.Repositories.Interfaces;
using RabbitReview.Services.Interfaces;

namespace RabbitReview.Services
{

    public class MyMessageService : IMyMessageService
    {
        public IMyMessageRepository _messageRepository { get; set; }

        public MyMessageService(IMyMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }        

        public async Task SendMessage(MyMessage message)
        {
            await _messageRepository.PublishMessage(message);
        }

        public async Task SendMessage(MyMessage message, string exchange)
        {
            await _messageRepository.PublishMessage(message,exchange);
        }
    }
}