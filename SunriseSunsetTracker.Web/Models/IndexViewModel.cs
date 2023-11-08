namespace SunriseSunsetTracker.Web.Models;

public class IndexViewModel
{
    public List<Card> Cards { get; init; } = new List<Card>();
}

public class Card
{
    public required string Name { get; set; }
    public required (double Latitude, double Longitude) GeoCoordinates { get; set; }
}
