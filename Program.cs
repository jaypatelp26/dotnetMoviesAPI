using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Driver;
using MoviesAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Register MongoDB client
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    return new MongoClient(builder.Configuration.GetConnectionString("MongoDB"));
});

// Register MoviesDbContext
builder.Services.AddSingleton<MoviesDBContext>();

// Register MoviesService as the implementation of IMoviesService
builder.Services.AddSingleton<IMoviesService, MoviesService>();

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
