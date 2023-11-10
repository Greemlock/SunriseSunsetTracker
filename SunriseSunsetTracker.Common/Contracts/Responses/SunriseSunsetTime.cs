namespace SunriseSunsetTracker.Common.Contracts.Responses;

public class SunriseSunsetTime
{
    public required DateTime SunriseTime { get; set; }
    public required DateTime SunsetTime { get; set; }
}

public class SunriseSunsetTimeFormatted : SunriseSunsetTime
{
    private readonly string _format;

    public string SunriseTimeFormatted => SunriseTime.ToString(_format);

    public string SunsetTimeFormatted => SunsetTime.ToString(_format);
    
    public SunriseSunsetTimeFormatted(string format) =>
        _format = format;
}