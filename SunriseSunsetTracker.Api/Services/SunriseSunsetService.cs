using System.Globalization;
using SunriseSunsetTracker.Api.Data.DTO;
using SunriseSunsetTracker.Api.Interfaces;
using SunriseSunsetTracker.Common.Extensions;

namespace SunriseSunsetTracker.Api.Services;

public class SunriseSunsetService : ISunriseSunsetService
{
    private readonly HttpClient _httpClient;

    public SunriseSunsetService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetSunriseSunsetResponse> GetSunriseSunsetTimeAsync(
        double latitude,
        double longitude)
    {
        var values = new Dictionary<string, string>
        {
            { "lat", latitude.ToString(CultureInfo.InvariantCulture) },
            { "lng", longitude.ToString(CultureInfo.InvariantCulture) }
        };

        var requestContent = await new FormUrlEncodedContent(values).ReadAsStringAsync();
        var response =  await _httpClient.GetAsync($"?{requestContent}");

        var result = await response.DeserializeResponseAsync<GetSunriseSunsetResponse>();

        return result;
    }
}