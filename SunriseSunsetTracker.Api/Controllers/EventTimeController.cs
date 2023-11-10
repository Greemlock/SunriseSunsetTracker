using Microsoft.AspNetCore.Mvc;
using SunriseSunsetTracker.Api.Interfaces;
using SunriseSunsetTracker.Common.Contracts.Responses;
using SunriseSunsetTracker.Common.Extensions;

namespace SunriseSunsetTracker.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EventTimeController : ControllerBase
{
    private readonly ISunriseSunsetService _sunriseSunsetService;

    public EventTimeController(
        ISunriseSunsetService sunriseSunsetService)
    {
        _sunriseSunsetService = sunriseSunsetService;
    }
    
    [HttpGet("sunrise&sunset")]
    public async Task<IActionResult> GetTodaySunriseSunsetTime(double latitude, double longitude)
    {
        if (!GeoHelper.IsValidGeoCoordinate(latitude, longitude))
            return BadRequest("Invalid geo-coordinates are specified!");
        
        var response = await _sunriseSunsetService.GetSunriseSunsetTimeAsync(latitude, longitude);
        
        return Ok(new SunriseSunsetTime
        {
            SunriseTime = response.Results.Sunrise,
            SunsetTime = response.Results.Sunset
        });
    }
    
    [HttpGet("sunrise")]
    public async Task<IActionResult> GetTodaySunriseTime(
        double latitude,
        double longitude)
    {
        if (!GeoHelper.IsValidGeoCoordinate(latitude, longitude))
            return BadRequest("Invalid geo-coordinates are specified!");
        
        var response = await _sunriseSunsetService.GetSunriseSunsetTimeAsync(latitude, longitude);
        return Ok(response.Results.Sunrise);
    }
    
    [HttpGet("sunset")]
    public async Task<IActionResult> GetTodaySunsetTime(
        double latitude,
        double longitude)
    {
        if (!GeoHelper.IsValidGeoCoordinate(latitude, longitude))
            return BadRequest("Invalid geo-coordinates are specified!");
        
        var response = await _sunriseSunsetService.GetSunriseSunsetTimeAsync(latitude, longitude);
        return Ok(response.Results.Sunset);
    }
}