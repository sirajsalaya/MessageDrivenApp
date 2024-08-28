using System.Threading.Tasks.Dataflow;
using MessageDrivenApp.Models;
using MessageDrivenApp.ProducerConsumer;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Register ProducerConsumer services
ProducerConsumerExtension.RegisterServices(builder.Services);

// Add services to the container.
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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (
    [FromServices] Producer producer,
    [FromServices] ITargetBlock<Message> messageQueue) =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    var message = new Message(string.Join<WeatherForecast>(",", forecast));
    producer.Produce(messageQueue, message);
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/metrics", (
    [FromServices] Consumer consumer) =>
{
    return Results.Ok(new
    {
        consumer.SuccessCount,
        consumer.ErrorCount
    });
})
.WithName("metrics")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
