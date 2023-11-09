using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using SunriseSunsetTracker.Common.Contracts.Responses;
using SunriseSunsetTracker.Common.Database.Entities;
using SunriseSunsetTracker.Web.Interfaces;
using SunriseSunsetTracker.Web.Models;

namespace SunriseSunsetTracker.Web.Controllers;

public class HomeController : Controller
{
    private readonly ICityService _cityService;
    private readonly IEventTimeService _eventTimeService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ICityService cityService, IEventTimeService eventTimeService, ILogger<HomeController> logger)
    {
        _cityService = cityService;
        _eventTimeService = eventTimeService;
        _logger = logger;
    }

    public async Task<ViewResult> Index()
    {
        var viewModel = new IndexViewModel()
        {
            Cities = await _cityService.GetAllCityAsync()
        };
            
        return View(viewModel);
    }
    
    [HttpGet("eventtime")]
    public async Task<EventTimeResponseModel> GetSunriseSunsetTime(
        string latitude,
        string longitude)
    {
        return await _eventTimeService.GetSunsetSunriseTimeAsync(
            double.Parse(latitude, CultureInfo.InvariantCulture),
            double.Parse(longitude, CultureInfo.InvariantCulture));
    }

    #region City management

    [HttpGet("{id}")]
    public async Task<City> Put(int id) =>
        await _cityService.GetCitByIdAsync(id);
    
    [HttpPut("update")]
    public async Task<RedirectToActionResult> Put(int id, string name, string longitude, string latitude)
    {
        await _cityService.UpdateCityAsync(new City
        {
            Id = id,
            Name = name,
            Latitude = double.Parse(longitude, CultureInfo.InvariantCulture),
            Longitude = double.Parse(latitude, CultureInfo.InvariantCulture)
        });
        
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost("add")]
    public async Task<RedirectToActionResult> Post(string name, string longitude, string latitude)
    {
        await _cityService.AddCityAsync(new City
        {
            Name = name,
            Latitude = double.Parse(longitude, CultureInfo.InvariantCulture),
            Longitude = double.Parse(latitude, CultureInfo.InvariantCulture)
        });
        
        return RedirectToAction(nameof(Index));
    }
    
    [HttpDelete("{id}")]
    public async Task<RedirectToActionResult> Delete(int id)
    {
        await _cityService.DeleteCityAsync(id);
        return RedirectToAction(nameof(Index));
    }

    #endregion
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}