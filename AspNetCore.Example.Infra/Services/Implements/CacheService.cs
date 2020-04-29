using AspNetCore.Example.Infra.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
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

        public async Task<bool> AddAsync<T>(string key, T value, DateTimeOffset expiresAt) where T : class
        {
            List<T> lista = new List<T>();
            lista.Add(value);

            if (database.KeyExists(key))
            {
                lista.AddRange(await GetListAsync<T>(key));
            }

            var serializedObject = JsonConvert.SerializeObject(lista);
            var expiration = expiresAt.Subtract(DateTimeOffset.Now);

            return await database.StringSetAsync(key, serializedObject, expiration);
        }

        public async Task<T> Get<T>(string key) where T : class
        {
            var serializedObject = await database.StringGetAsync(key);

            return JsonConvert.DeserializeObject<T>(serializedObject);
        }

        public async Task<List<T>> GetListAsync<T>(string key) where T : class
        {
            var serializedObject = await database.StringGetAsync(key);
            return JsonConvert.DeserializeObject<List<T>>(serializedObject);
        }
       
        public void Push<T>(string key, T value, DateTimeOffset expiresAt) where T : class
        {
            var serializedObject = JsonConvert.SerializeObject(value);
            var expiration = expiresAt.Subtract(DateTimeOffset.Now);

            database.StringAppend(key, serializedObject);
        }

        public  async Task PushInList(string key, string value)
        {            
            await database.ListRightPushAsync(key, value);
        }

        public  async Task HashSetAsync<T>(string key, string hashKey, List<T> documentos) where T : class
        {            
            HashEntry[] _hashEntry = { new HashEntry(hashKey, JsonConvert.SerializeObject(documentos)) };

            await database.HashSetAsync(key, _hashEntry);
        }

        public async Task<bool> Exists(string key)
        {
            if (await database.KeyExistsAsync(key))
            {
                return true;
            }

            return false;
        }

        public  async Task<List<T>> HashGetAsync<T>(string key, string cnpj) where T : class
        {            
            var serializedObject = await database.HashGetAsync(key, cnpj);
            return JsonConvert.DeserializeObject<List<T>>(serializedObject.ToString());
        }

        public static void HashGet(string key, string cnpj)
        {           
            var serializedObject = database.HashGet(key, cnpj);
        }
    }
}
