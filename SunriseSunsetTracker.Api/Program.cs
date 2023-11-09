using Microsoft.EntityFrameworkCore;
using SunriseSunsetTracker.Api.Data.Database;
using SunriseSunsetTracker.Api.Data.Database.Repositories;
using SunriseSunsetTracker.Api.Interfaces;
using SunriseSunsetTracker.Api.Services;
using SunriseSunsetTracker.Common.Database.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IEntityRepository<City>, CityRepository>();


builder.Services.AddHttpClient<ISunriseSunsetService, SunriseSunsetService>(client => 
{
    client.BaseAddress = new Uri(builder.Configuration["HttpClients:SunsetSunrise:BaseUrl"]!);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();