using Microsoft.AspNetCore.Mvc;
using SunriseSunsetTracker.Api.Interfaces;
using SunriseSunsetTracker.Common.Contracts.Responses;

namespace SunriseSunsetTracker.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EventTimeController : ControllerBase
{
    private readonly ISunriseSunsetService _sunriseSunsetService;

    public EventTimeController(ISunriseSunsetService sunriseSunsetService)
    {
        _sunriseSunsetService = sunriseSunsetService;
    }
    
    [HttpGet("latitude={latitude}&longitude={longitude}")]
    public async Task<IActionResult> GetTodaySunriseSunsetTime(double latitude, double longitude)
    {
        var response = await _sunriseSunsetService.GetSunriseSunsetTimeAsync(latitude, longitude);
        return Ok(new EventTimeResponseModel
        {
                SunriseTime = response.Results.Sunrise,
                SunsetTime = response.Results.Sunset
            });
    }
}