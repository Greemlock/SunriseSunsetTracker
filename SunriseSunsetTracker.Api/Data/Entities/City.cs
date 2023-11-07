namespace SunriseSunsetTracker.Api.Data.Entities;

public class City
{
    public required ulong  Id { get; init; }
    public required string Name { get; init; }
    public required (double Latitude, double Longitude) GeoCoordinates { get; init; }
}