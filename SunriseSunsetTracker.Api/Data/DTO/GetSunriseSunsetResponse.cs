using System.Net;
using Newtonsoft.Json;

namespace SunriseSunsetTracker.Api.Data.DTO;

public class GetSunriseSunsetResponse
{
    [JsonProperty("results")]
    public required SunriseSunsetInfo Results { get; init; }
    
    [JsonProperty("status")]
    public required HttpStatusCode Status { get; init; }
}

public class SunriseSunsetInfo
{
    [JsonProperty("sunrise")]
    public required DateTime Sunrise { get; init; }

    [JsonProperty("sunset")]
    public required DateTime Sunset { get; init; }

    [JsonProperty("solar_noon")]
    public required DateTime SolarNoon { get; init; }

    [JsonProperty("day_length")]
    public required string DayLength { get; init; }

    [JsonProperty("civil_twilight_begin")]
    public required DateTime CivilTwilightBegin { get; init; }

    [JsonProperty("civil_twilight_end")]
    public required DateTime CivilTwilightEnd { get; init; }

    [JsonProperty("nautical_twilight_begin")]
    public required DateTime NauticalTwilightBegin { get; init; }

    [JsonProperty("nautical_twilight_end")]
    public required DateTime NauticalTwilightEnd { get; init; }

    [JsonProperty("astronomical_twilight_begin")]
    public required DateTime AstronomicalTwilightBegin { get; init; }

    [JsonProperty("astronomical_twilight_end")]
    public required DateTime AstronomicalTwilightEnd { get; init; }
}