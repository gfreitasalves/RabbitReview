// See https://aka.ms/new-console-template for more information
using RabbitReview.Repositories;
using RabbitReview.Services;

Console.WriteLine("Messages:");

Console.WriteLine();


var myQueueItemReaderService = new MyQueueItemReaderService(new MyMessageRepository());
await myQueueItemReaderService.Subscribe();

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
