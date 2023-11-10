using Microsoft.AspNetCore.Mvc;
using SunriseSunsetTracker.Api.Interfaces;
using SunriseSunsetTracker.Common.Database.Entities;

namespace SunriseSunsetTracker.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CityController : ControllerBase
{
    private readonly IEntityRepository<City> _cityRepository;

    public CityController(IEntityRepository<City> cityRepository)
    {
        _cityRepository = cityRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCity(int id)
    {
        var entity = await _cityRepository.GetByIdAsync(id);
        return entity is not null 
            ? Ok(entity)
            : NotFound(entity);
    }
    
    [HttpGet("list")]
    public async Task<IActionResult> GetAllCities()
    {
        var entity = await _cityRepository.GetAllAsync();
        return entity.Any() 
            ? Ok(entity)
            : NotFound(entity);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddCity([FromBody] City? entity)
    {
        if (entity is null)
            return BadRequest("City entity cannot be null.");

        await _cityRepository.AddAsync(entity);
        return Created($"City {entity} has been added.", entity);
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> UpdateCity([FromBody] City? entity)
    {
        if (entity is null)
            return BadRequest("City entity cannot be null.");
        
        var existingCity = await _cityRepository.GetByIdAsync(entity.Id);
        if (existingCity == null)
            return NotFound($"City with ID {entity.Id} not found.");

        await _cityRepository.UpdateAsync(entity);
        return Ok(entity);
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteCity(int id)
    {
        var entity = await _cityRepository.GetByIdAsync(id);
        if (entity is null)
            return NotFound($"City with id {id} not found.");
        
        await _cityRepository.RemoveAsync(id);
        return Ok($"City {id} has been deleted.");
    }
}