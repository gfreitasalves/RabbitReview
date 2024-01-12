// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using RabbitReview.Models;
using System.Text.Json;
using System.Text;

Console.WriteLine("Messages:");

Console.WriteLine();


var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "admin",
    Password = "123456"
};
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "rabbitMessageQueue",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var json = Encoding.UTF8.GetString(body);

        MyMessage message = JsonSerializer.Deserialize<MyMessage>(json);

        Console.WriteLine($"{message.Id} = Titulo: {message.Title}; Texto={message.Description}");
    };
    channel.BasicConsume(queue: "rabbitMessageQueue",
                         autoAck: true,
                         consumer: consumer);



    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
}