using RabbitReview.Models;
using RabbitReview.Repositories;
using RabbitReview.Repositories.Interfaces;
using RabbitReview.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace RabbitReview.Services
{
    public class MyQueueItemReaderService : IMyQueueItemReaderService
    {
        public IMyMessageRepository _myMessageRepository { get; }

        public MyQueueItemReaderService(IMyMessageRepository myMessageRepository)
        {
            _myMessageRepository = myMessageRepository;
        }

        public async Task Subscribe()
        {
            await _myMessageRepository.SubscribeMessages(ReadItem);
        }

        public async Task Subscribe(string exchange)
        {
            await _myMessageRepository.SubscribeMessages(ReadItem);
        }

        private void ReadItem(byte[] body)
        {

            var json = Encoding.UTF8.GetString(body);

            MyMessage message = JsonSerializer.Deserialize<MyMessage>(json);

            Console.WriteLine($"{message.Id} = Title: {message.Title}; Description={message.Description}");
        }        
    }
}