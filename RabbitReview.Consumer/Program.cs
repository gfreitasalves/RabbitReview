// See https://aka.ms/new-console-template for more information
using RabbitReview.Repositories;

Console.WriteLine("Messages:");

Console.WriteLine();


var connection = new MyMessageRepository();
await connection.ReadMessages(new MyQueueItemReader());

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
