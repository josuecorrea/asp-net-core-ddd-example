using AspNetCore.Example.Infra.Services.Contracts;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace AspNetCore.Example.Infra.Services.Implements
{
    public class CacheService : ICacheService
    {
        private static ConnectionMultiplexer connectionMultiplexer;
        private readonly IConfiguration _configuration;
        private static IDatabase database;

        public CacheService(IConfiguration configuration)
        {
            _configuration = configuration;
            Configure();
        }

        private void Configure()
        {
            var connectionString = string.Format("{0}:{1}", "localhost", 6379);

            connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
            database = connectionMultiplexer.GetDatabase();
        }

        public async Task<bool> StoreDataAsync(string key, string value)
        {
            return await database.StringSetAsync(key, value);
        }

        public async Task<string> GetDataAsync(string key)
        {
            return await database.StringGetAsync(key);
        }

        public async void DeleteDataAsync(string key)
        {
            await database.KeyDeleteAsync(key);
        }   

        public async Task<bool> Exists(string key)
        {
            if (await database.KeyExistsAsync(key))
            {
                return true;
            }

            return false;
        }       
    }
}
