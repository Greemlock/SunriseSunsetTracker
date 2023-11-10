using System.Globalization;
using SunriseSunsetTracker.Common.Contracts.Responses;
using SunriseSunsetTracker.Common.Extensions;
using SunriseSunsetTracker.Web.Interfaces;

namespace SunriseSunsetTracker.Web.Services;

public class EventTimeService : IEventTimeService
{
    private readonly HttpClient _httpClient;

    public EventTimeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<SunriseSunsetTime> GetSunriseSunsetTimeAsync(double latitude, double longitude)
    {
        var values = new Dictionary<string, string>
        {
            { nameof(latitude), latitude.ToString(CultureInfo.InvariantCulture) },
            { nameof(longitude), longitude.ToString(CultureInfo.InvariantCulture) }
        };
        var requestContent = await new FormUrlEncodedContent(values).ReadAsStringAsync();
        var response = await _httpClient.GetAsync($"sunrise&sunset?{requestContent}");
        
        return await response.DeserializeResponseAsync<SunriseSunsetTime>();
    }

    public async Task<DateTime> GetSunriseSTimeAsync(double latitude, double longitude)
    {
        var values = new Dictionary<string, string>
        {
            { nameof(latitude), latitude.ToString(CultureInfo.InvariantCulture) },
            { nameof(longitude), longitude.ToString(CultureInfo.InvariantCulture) }
        };
        var requestContent = await new FormUrlEncodedContent(values).ReadAsStringAsync();
        var response = await _httpClient.GetAsync($"sunrise?{requestContent}");
        
        if (!DateTime.TryParse(await response.Content.ReadAsStringAsync(), out var sunriseTime))
            throw new FormatException("Invalid time format in server response!");

        return sunriseTime;
    }

    public async Task<DateTime> GetSunsetTimeAsync(double latitude, double longitude)
    {
        var values = new Dictionary<string, string>
        {
            { nameof(latitude), latitude.ToString(CultureInfo.InvariantCulture) },
            { nameof(longitude), longitude.ToString(CultureInfo.InvariantCulture) }
        };
        var requestContent = await new FormUrlEncodedContent(values).ReadAsStringAsync();
        var response = await _httpClient.GetAsync($"sunset?{requestContent}");
        
        if (!DateTime.TryParse(await response.Content.ReadAsStringAsync(), out var sunsetTime))
            throw new FormatException("Invalid time format in server response!");
        
        return sunsetTime;
    }
}