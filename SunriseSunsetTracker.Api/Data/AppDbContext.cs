using Microsoft.EntityFrameworkCore;
using SunriseSunsetTracker.Api.Data.Entities;

namespace SunriseSunsetTracker.Api.Data;

public sealed class AppDbContext : DbContext
{
    public required DbSet<City> Cities { get; init; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) =>
        Database.EnsureCreated();
}