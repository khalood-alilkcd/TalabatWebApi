using Service.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service
{
    public class ResponseCacheService : IResponseCacheService
    {
        /// <summary>
        ///  this service serve the database to not return again
        ///  the same data like get data from hard desk then transfer to ram and cache the data  
        /// </summary>
        private readonly IDatabase _database;
        public ResponseCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan time)
        {
            // i check if response is null and return null
            if (response == null) return;
            /// i intialize new instance of JsonSerializerOptions to store object like CamelCase
            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            // serialize response (convert object Json) to cacheing and set it to database redis 
            var serializedResponse = JsonSerializer.Serialize(response, options);
            
            await _database.StringSetAsync(cacheKey, serializedResponse, time);
        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var cachedResponse = await _database.StringGetAsync(cacheKey);
            // cachedResponse is return RedisValue and its struct and it's not return null so must return null like code bellow
            if (cachedResponse.IsNullOrEmpty) return null;

            return cachedResponse;
        }
    }
}
