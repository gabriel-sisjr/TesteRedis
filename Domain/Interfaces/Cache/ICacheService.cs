using System.Threading.Tasks;

namespace Domain.Interfaces.Cache
{
    public interface ICacheService
    {
        Task<T> GetFromCacheAsync<T>(string key);
    }
}