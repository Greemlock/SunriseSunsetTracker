using SunriseSunsetTracker.Common.Contracts.Responses;

namespace SunriseSunsetTracker.Web.Interfaces;

public interface IEventTimeService
{
    public Task<SunriseSunsetTime> GetSunriseSunsetTimeAsync(double latitude, double longitude);
    public Task<DateTime> GetSunriseSTimeAsync(double latitude, double longitude);
    public Task<DateTime> GetSunsetTimeAsync(double latitude, double longitude);
}