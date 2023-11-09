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
    public async Task<EventTimeResponseModel> GetSunsetSunriseTimeAsync(double latitude, double longitude)
    {
        var response = await _httpClient.GetAsync($"latitude={latitude}&longitude={longitude}");
        return await response.DeserializeResponseAsync<EventTimeResponseModel>();
    }
}