using Microsoft.AspNetCore.Mvc;
using SunriseSunsetTracker.Api.Interfaces;

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
    public async Task<IActionResult> GetTodaySunriseSunsetTime(float latitude, float longitude)
    {
        var response = await _sunriseSunsetService.GetSunriseSunsetTime(latitude, longitude);
        
        return Ok(new
        {
            response.Results.Sunrise,
            response.Results.Sunset
        });
    }
}