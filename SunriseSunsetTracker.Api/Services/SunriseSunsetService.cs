using System.Globalization;
using SunriseSunsetTracker.Api.Data.DTO;
using SunriseSunsetTracker.Api.Interfaces;
using SunriseSunsetTracker.Api.Utils.Extensions;

namespace SunriseSunsetTracker.Api.Services;

public class SunriseSunsetService : ISunriseSunsetService
{
    private readonly HttpClient _httpClient;

    public SunriseSunsetService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetSunriseSunsetResponse> GetSunriseSunsetTime(
        float latitude,
        float longitude)
    {
        var values = new Dictionary<string, string>
        {
            { nameof(latitude), latitude.ToString(CultureInfo.InvariantCulture) },
            { nameof(longitude), longitude.ToString(CultureInfo.InvariantCulture) }
        };

        var requestContent = await new FormUrlEncodedContent(values).ReadAsStringAsync();
        var response =  await _httpClient.GetAsync($"?{requestContent}");

        var result = await response.DeserializeResponseAsync<GetSunriseSunsetResponse>();

        return result;
    }
}