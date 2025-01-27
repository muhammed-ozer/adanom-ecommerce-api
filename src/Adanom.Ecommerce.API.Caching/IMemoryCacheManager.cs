namespace Adanom.Ecommerce.API.Caching
{
    public interface IMemoryCacheManager
    {
        T Get<T>(string key);

        void Set<T>(string key, T value, CacheOptions options);

        void Remove(string key);

        void RemoveByPattern(string pattern);

        void RemoveByRegion(string region);

        bool TryGet<T>(string key, out T value);

        void Clear();

        bool Contains(string key);

        IEnumerable<string> GetKeys(string? region = null);
    }
}