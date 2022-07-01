using Microsoft.EntityFrameworkCore;
using Order.Logic.Persistence;
using Order.Logic.Persistence.Dtos;
using ordering_server.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("OrderDbContextConnection") ?? throw new InvalidOperationException("Connection string 'OrderDbContextConnection' not found.");

builder.Configuration["ConfigurationString:RabbitMQString"] = builder.Configuration.GetValue<string>("ConfigurationString:RabbitMQString");

builder.Services.Configure<ConfigurationString>(builder.Configuration.GetSection(nameof(ConfigurationString)));

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddScoped<IMessageProducer, RabbitMQProducer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(option =>
{
    option.AddPolicy("enable", opt =>
    {
        opt.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin();
    });
});

// Use auto-mapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("enable");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
