using SunriseSunsetTracker.Common.Database.Entities;

namespace SunriseSunsetTracker.Web.Models;

public class IndexViewModel
{
    public required IEnumerable<City> Cities { get; init; }
}