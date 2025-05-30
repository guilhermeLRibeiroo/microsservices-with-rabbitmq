using ItemService.AsyncDataServices;
using ItemService.Data;
using ItemService.Data.Interfaces;
using ItemService.Data.Repositories;
using ItemService.EventProcessing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DefaultDbContext>(opt => opt.UseInMemoryDatabase("InMemoryDatabase"));

RabbitMQConfiguration rabbitmqConfig = new();
builder.Configuration.GetSection(nameof(RabbitMQConfiguration)).Bind(rabbitmqConfig);
builder.Services.AddSingleton(rabbitmqConfig);

builder.Services.AddSingleton<ICreateRestaurantEvent, CreateRestaurantEvent>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHostedService<MessageBusSubscriber>();

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
