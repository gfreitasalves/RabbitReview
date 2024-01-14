using RabbitReview.Services.Interfaces;

namespace RabbitReview.Consumer.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMyQueueItemReaderService _myQueueItemReaderService;

        public Worker(ILogger<Worker> logger, IMyQueueItemReaderService myQueueItemReaderService)
        {
            _logger = logger;
            _myQueueItemReaderService = myQueueItemReaderService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _myQueueItemReaderService.Subscribe();

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"Is Up {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}.");
                Thread.Sleep(5000);
            }
        }
    }
}