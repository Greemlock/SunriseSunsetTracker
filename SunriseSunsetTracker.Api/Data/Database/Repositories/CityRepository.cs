using Microsoft.EntityFrameworkCore;
using SunriseSunsetTracker.Api.Interfaces;
using SunriseSunsetTracker.Common.Database.Entities;

namespace SunriseSunsetTracker.Api.Data.Database.Repositories;

public class CityRepository : IEntityRepository<City>
{
    private readonly AppDbContext _dbContext;

    public CityRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<City?>> GetAllAsync() =>
        await _dbContext.Cities.ToListAsync();

    public async Task<City?> GetByIdAsync(int id) =>
        await _dbContext.Cities.FindAsync(id);

    public async Task AddAsync(City city)
    {
        await _dbContext.Cities.AddAsync(city);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(City city)
    {
        _dbContext.Cities.Update(city);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(City city)
    {
        _dbContext.Cities.Remove(city);
        await _dbContext.SaveChangesAsync();
    }
}