using SunriseSunsetTracker.Api.Data.DTO;

namespace SunriseSunsetTracker.Api.Interfaces;

public interface ISunriseSunsetService
{
    public Task<GetSunriseSunsetResponse> GetSunriseSunsetTimeAsync(
        double latitude,
        double longitude);
}