using Microsoft.EntityFrameworkCore;
using SunriseSunsetTracker.Common.Database.Entities;
using SunriseSunsetTracker.Common.Extensions;

namespace SunriseSunsetTracker.Api.Data.Database;

public sealed class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public required DbSet<City> Cities { get; set; }

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
        
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Building City model

        modelBuilder.Entity<City>(entity => entity
            .ToTable(nameof(City))
            .HasKey(city => city.Id)
        );
        
        modelBuilder.Entity<City>()
            .Property(entity => entity.Name)
            .IsRequired();

        modelBuilder.Entity<City>()
            .Property(entity => entity.Latitude)
            .IsRequired();
        
        modelBuilder.Entity<City>()
            .Property(entity => entity.Longitude)
            .IsRequired();
        
        var citySeedDataPath =
            Directory.GetCurrentDirectory() +
            Path.Combine(
                _configuration["DefaultInitDataPath"]!,
                $"{nameof(City)}.json");

        modelBuilder
            .Entity<City>().HasData(DbHelper.GetSeedDataAsync<City>(citySeedDataPath).Result);

        #endregion
    }
}