using System.Text.Json;
using Application.Common.Interfaces;
using StackExchange.Redis;

namespace MicrblogApp.Infrastructure.Services;

public class CacheService : ICacheService
{
    private readonly IDatabase _database;
    private readonly ConnectionMultiplexer _redis;

    public CacheService(ConnectionMultiplexer redis)
    {
        _redis = redis;
        _database = redis.GetDatabase();
    }
    public T GetFromCache<T>(string key) where T : class
    {
       
        var cachedResponse = _database.StringGet(key);
        return cachedResponse.IsNullOrEmpty ? null : JsonSerializer.Deserialize<T>(cachedResponse);
    }

    public IEnumerable<string> GetFromCache(string key)
    {
        var cachedResponse = _database.StringGet(key);
        return cachedResponse.IsNullOrEmpty
            ? Enumerable.Empty<string>()
            : JsonSerializer.Deserialize<List<string>>(cachedResponse);
    }

    public void SetCache<T>(string key, T value, int time = 0) where T : class
    {
        if(string.IsNullOrEmpty(key)) return;
        var response = JsonSerializer.Serialize(value);
        var cachingTime = time == 0 ? TimeSpan.FromHours(360) : TimeSpan.FromHours(time);
        _database.StringSetAsync(key, response, cachingTime);
    }
}