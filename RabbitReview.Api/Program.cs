using RabbitReview.Models;
using RabbitReview.Repositories;
using RabbitReview.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddTransient<IMessageRepository, MessageRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/publishMessage", async (Message message, IMessageService messageService) =>
{
    await messageService.SendMessage(message);
})
.WithName("PublishMessage");

app.Run();

