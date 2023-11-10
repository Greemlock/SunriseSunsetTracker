using SunriseSunsetTracker.Common.Database.Entities;

namespace SunriseSunsetTracker.Web.Interfaces;

public interface ICityService
{
    Task<City> GetCitByIdAsync(int id);
    Task<IEnumerable<City>> GetAllCityAsync();
    Task<City> UpdateCityAsync(City city);
    Task<City> AddCityAsync(City city);
    Task DeleteCityAsync(int id);
}