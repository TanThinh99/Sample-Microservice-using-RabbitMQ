using BackOffice.Persistence;
using BackOffice.RabbitMQ;
using Microsoft.EntityFrameworkCore;
using BackOffice.Persistence.Dtos;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("OrderBackOfficeDbContextConnection") ?? throw new InvalidOperationException("Connection string 'OrderBackOfficeDbContextConnection' not found.");

builder.Configuration["ConfigurationString:RabbitMQString"] = builder.Configuration.GetValue<string>("ConfigurationString:RabbitMQString");

builder.Services.Configure<ConfigurationString>(builder.Configuration.GetSection(nameof(ConfigurationString)));

builder.Services.AddDbContext<BackOfficeDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMessageProducer, RabbitMQProducer>();

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
