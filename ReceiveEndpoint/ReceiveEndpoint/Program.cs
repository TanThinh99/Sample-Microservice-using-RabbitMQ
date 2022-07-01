using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using ReceiveEndpoint.RabbitMQ;
using ReceiveEndpoint.Persistence.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigurationString config = new()
{
    RabbitMQString = builder.Configuration.GetValue<string>("ConfigurationString:RabbitMQString"),
    BackOfficeAddress = builder.Configuration.GetValue<string>("ConfigurationString:BackOfficeAddress"),
};

var consumer = new Consumer(new ManualResetEvent(false), new HttpClient(), config, builder.Configuration);
var consumerThread = new Thread(consumer.ConsumeQueue) { IsBackground = true };
consumerThread.Start();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
