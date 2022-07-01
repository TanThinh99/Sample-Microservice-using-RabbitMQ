using Inventory.Persistence;
using Inventory.Persistence.Dtos;
using Inventory.RabbitMQ;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("InventoryDbContextConnection") ?? throw new InvalidOperationException("Connection string 'InventoryDbContextConnection' not found.");

builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(connectionString));

ConfigurationString config = new() 
{ 
    RabbitMQString = builder.Configuration.GetValue<string>("ConfigurationString:RabbitMQString"),
    BaseAddress = builder.Configuration.GetValue<string>("ConfigurationString:BaseAddress")
};

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var consumer = new Consumer(new ManualResetEvent(false), new HttpClient(), config);
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
