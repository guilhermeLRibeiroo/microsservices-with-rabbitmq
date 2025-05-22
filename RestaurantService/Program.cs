using ItemService.Data;
using ItemService.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using RestaurantService.AsyncDataServices;
using RestaurantService.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("RestaurantConnection");

builder.Services.AddDbContext<DefaultDbContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

RabbitMQConfiguration rabbitmqConfig = new();
builder.Configuration.GetSection(nameof(RabbitMQConfiguration)).Bind(rabbitmqConfig);
builder.Services.AddSingleton(rabbitmqConfig); 

builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DefaultDbContext>();
    db.Database.Migrate();
}

app.Run();
