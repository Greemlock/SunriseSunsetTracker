using Microsoft.AspNetCore.Mvc;
using SunriseSunsetTracker.Api.Data.Database.Entities;
using SunriseSunsetTracker.Api.Interfaces;

namespace SunriseSunsetTracker.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CityController : ControllerBase
{
    private readonly IEntityRepository<City> _cityRepository;
    private readonly ILogger<CityController> _logger;

    public CityController(IEntityRepository<City> cityRepository, ILogger<CityController> logger)
    {
        _cityRepository = cityRepository;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCity(int id)
    {
        var entity = await _cityRepository.GetByIdAsync(id);
        return entity is not null 
            ? Ok(entity)
            : NotFound(entity);
    }
    //
    // await Task.FromResult(new GetCityResponseModel
    //     {
    //         City = new City()
    //         {
    //             Id = 1,
    //             Name = "WeatherForecast",
    //             GeoCoordinates = (1, 1)
    //         }
    //     });
    //
    // [HttpGet]
    // public async Task<GetAllCitiesResponseModel> GetAllCities() => 
    //     await Task.FromResult(new GetAllCitiesResponseModel
    //     {
    //         Cities = new List<City>()
    //         {
    //             new City()
    //             {
    //                 Id = 1,
    //                 Name = "WeatherForecast",
    //                 GeoCoordinates = (1, 1)
    //             },
    //             new City()
    //             {
    //                 Id = 2,
    //                 Name = "WeatherForecast",
    //                 GeoCoordinates = (2, 2)
    //             }
    //         },
    //     });
    //
    // [HttpPost]
    // public async Task<IActionResult> AddCity(AddCityRequest request) => await Task.FromResult(Created(request.City.Id.ToString(), request.City));
    //
    // [HttpPost]
    // public async Task<IActionResult> AddMultipleCities(AddMultipleCitiesRequest request) => await Task.FromResult(Created());
}