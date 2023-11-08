namespace SunriseSunsetTracker.Api.Data.Database.Entities;

public class City
{
    public required int  Id { get; init; }
    public required string Name { get; init; }
    public required string GeoHash { get; init; }
}