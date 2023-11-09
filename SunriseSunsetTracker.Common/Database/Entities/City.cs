namespace SunriseSunsetTracker.Common.Database.Entities;

public class City
{
    public int  Id { get; init; }
    public string Name { get; init; } = null!;
    public double Latitude { get; init; }
    public double Longitude { get; init; }
}