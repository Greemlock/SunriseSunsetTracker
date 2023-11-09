using SunriseSunsetTracker.Common.Contracts.Responses;

namespace SunriseSunsetTracker.Web.Interfaces;

public interface IEventTimeService
{
    public Task<EventTimeResponseModel> GetSunsetSunriseTimeAsync(double latitude, double longitude);
}