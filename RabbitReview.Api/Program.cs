using RabbitReview.Models;
using RabbitReview.Repositories;
using RabbitReview.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IMyMessageService, MyMessageService>();
builder.Services.AddTransient<IMyMessageRepository, MyMessageRepository>();

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

app.Run();

