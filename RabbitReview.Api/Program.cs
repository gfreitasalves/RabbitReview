using RabbitReview.Models;
using RabbitReview.Repositories;
using RabbitReview.Repositories.Interfaces;
using RabbitReview.Services;
using RabbitReview.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IMyMessageService, MyMessageService>();
builder.Services.AddTransient<IMyMessageRepository, MyMessageRepository>();
builder.Services.AddTransient<IMyQueueItemReaderService, MyQueueItemReaderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/publishMyMessage", async (MyMessage message, IMyMessageService messageService) =>
{
    await messageService.SendMessage(message);
})
.WithName("PublishMyMessage");

app.MapPost("/publishMyMessageExchange/{exchange}", async (MyMessage message, string exchange, IMyMessageService messageService) =>
{
    await messageService.SendMessage(message,exchange);
})
.WithName("PublishMyMessageExchange");



app.Run();

