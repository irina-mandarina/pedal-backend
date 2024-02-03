using Pedal.Models;
using Pedal.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<StoreDatabaseSettings>(
    builder.Configuration.GetSection("PedalDatabase"));

builder.Services.AddSingleton<CarRepository>();
builder.Services.AddSingleton<SwipeRepository>();
builder.Services.AddSingleton<MessageRepository>();

// Add services to the container.

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

app.Run();
