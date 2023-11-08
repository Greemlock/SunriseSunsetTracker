using Microsoft.EntityFrameworkCore;
using SunriseSunsetTracker.Api.Data.Database.Entities;
using SunriseSunsetTracker.Api.Interfaces;

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

    public async Task AddAsync(City? product)
    {
        await _dbContext.Cities.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(City? product)
    {
        _dbContext.Cities.Update(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var product = await _dbContext.Cities.FindAsync(id);
        _dbContext.Cities.Remove(product);
        await _dbContext.SaveChangesAsync();
    }
}