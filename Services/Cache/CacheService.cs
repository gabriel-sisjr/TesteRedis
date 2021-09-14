using Domain.Interfaces.Cache;
using Domain.Models;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        public CacheService(IDistributedCache distributedCache) => _distributedCache = distributedCache;

        public async Task<T> GetFromCacheAsync<T>(string key)
        {
            var resultCache = await _distributedCache.GetAsync(key);
            var converted = Encoding.UTF8.GetString(resultCache ?? Array.Empty<byte>());

            if (string.IsNullOrWhiteSpace(converted))
            {
                // Simulation the object of database.
                var objJson = JsonSerializer.Serialize(new Retorno { Id = int.Parse(key), Message = $"Cache ID: {key}" });
                var valueFromDB = Encoding.UTF8.GetBytes(objJson);
                await _distributedCache.SetAsync(key, valueFromDB, new DistributedCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(15),
                    AbsoluteExpiration = DateTime.Now.AddHours(5)
                });

                converted = Encoding.UTF8.GetString(valueFromDB);
                return JsonSerializer.Deserialize<T>(converted);
            }

            return JsonSerializer.Deserialize<T>(converted);
        }
    }
}
