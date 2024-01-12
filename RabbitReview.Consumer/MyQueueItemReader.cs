// See https://aka.ms/new-console-template for more information
using RabbitReview.Models;
using System.Text.Json;
using System.Text;
using RabbitReview.Repositories;

class MyQueueItemReader : IQueueConsumer
{
    public void ReadItem(byte[] body)
    {
        var json = Encoding.UTF8.GetString(body);

        MyMessage message = JsonSerializer.Deserialize<MyMessage>(json);

        Console.WriteLine($"{message.Id} = Title: {message.Title}; Description={message.Description}");
    }
}