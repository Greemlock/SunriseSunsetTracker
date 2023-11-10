using SunriseSunsetTracker.Common.Database.Entities;

namespace SunriseSunsetTracker.Api.Interfaces;

public interface IEntityRepository<in TEntity>
{
    public Task<IEnumerable<City?>> GetAllAsync();
    public Task<City?> GetByIdAsync(int id);
    public Task AddAsync(TEntity product);
    public Task UpdateAsync(TEntity product);
    public Task RemoveAsync(int id);
}