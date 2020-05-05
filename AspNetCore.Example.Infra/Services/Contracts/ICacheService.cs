using System.Threading.Tasks;

namespace AspNetCore.Example.Infra.Services.Contracts
{
    public interface ICacheService
    {
        Task<bool> StoreDataAsync(string key, string value);
        Task<string> GetDataAsync(string key);
        void DeleteDataAsync(string key);    
        Task<bool> Exists(string key);    
    }
}
