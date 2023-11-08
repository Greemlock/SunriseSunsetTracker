using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SunriseSunsetTracker.Api.Utils.Extensions;

public static class DbHelper
{
    public static async Task<List<TEntity>> GetSeedDataAsync<TEntity>(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File {filePath} not found.");

        using var reader = new StreamReader(filePath);
        var json = await reader.ReadToEndAsync();

        return JsonConvert.DeserializeObject<List<TEntity>>(json) ??
               throw new SerializationException("Failed to deserialize JSON data.");
    }
}