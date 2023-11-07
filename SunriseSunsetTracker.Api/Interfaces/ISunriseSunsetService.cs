using SunriseSunsetTracker.Api.Data.DTO;

namespace SunriseSunsetTracker.Api.Interfaces;

public interface ISunriseSunsetService
{
    public Task<GetSunriseSunsetResponse> GetSunriseSunsetTime(
        float latitude,
        float longitude);
}