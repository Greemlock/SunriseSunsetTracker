using SunriseSunsetTracker.Common.Database.Entities;
using SunriseSunsetTracker.Common.Extensions;
using SunriseSunsetTracker.Web.Interfaces;

namespace SunriseSunsetTracker.Web.Services;

public class CityService : ICityService
{
    private readonly HttpClient _httpClient;

    public CityService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<City> GetCitByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"{id}");
        return await response.DeserializeResponseAsync<City>();
    }

    public async Task<IEnumerable<City>> GetAllCityAsync()
    {
        var response = await _httpClient.GetAsync("list");
        return await response.DeserializeResponseAsync<IEnumerable<City>>();
    }

    public async Task<City> UpdateCityAsync(City city)
    {
        var response = await _httpClient.PutAsJsonAsync("update", city);
        return await response.DeserializeResponseAsync<City>();
    }

    public async Task<City> AddCityAsync(City city)
    {
        var response = await _httpClient.PostAsJsonAsync("add", city);
        return await response.DeserializeResponseAsync<City>();
    }

    public async Task DeleteCityAsync(int id)
    {
        await _httpClient.DeleteAsync($"delete/{id}");
    }
}