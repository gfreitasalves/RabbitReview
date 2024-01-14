using RabbitReview.Consumer.Worker;
using RabbitReview.Repositories.Interfaces;
using RabbitReview.Repositories;
using RabbitReview.Services.Interfaces;
using RabbitReview.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        services.AddTransient<IMyMessageService, MyMessageService>();
        services.AddTransient<IMyMessageRepository, MyMessageRepository>();
        services.AddTransient<IMyQueueItemReaderService, MyQueueItemReaderService>();
    })
    .Build();

await host.RunAsync();
