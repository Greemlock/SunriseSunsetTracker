using Microsoft.EntityFrameworkCore;
using SunriseSunsetTracker.Api.Data;
using SunriseSunsetTracker.Api.Interfaces;
using SunriseSunsetTracker.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<ISunriseSunsetService, SunriseSunsetService>(client => 
{
    client.BaseAddress = new Uri(builder.Configuration["HttpClients:SunsetSunrise:BaseUrl"]!);
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration["Database:DefaultConnectionString"]));

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