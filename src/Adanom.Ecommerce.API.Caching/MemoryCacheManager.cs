using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Memory;

namespace Adanom.Ecommerce.API.Caching
{
    internal sealed class MemoryCacheManager : IMemoryCacheManager
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ConcurrentDictionary<string, HashSet<string>> _regionKeys;

        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _regionKeys = new ConcurrentDictionary<string, HashSet<string>>();
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public void Set<T>(string key, T value, CacheOptions options)
        {
            var memoryCacheEntryOptions = new MemoryCacheEntryOptions();

            if (options.SlidingExpiration.HasValue)
                memoryCacheEntryOptions.SlidingExpiration = options.SlidingExpiration.Value;

            if (options.AbsoluteExpiration.HasValue)
                memoryCacheEntryOptions.AbsoluteExpirationRelativeToNow = options.AbsoluteExpiration.Value;

            if (!string.IsNullOrEmpty(options.Region))
            {
                _regionKeys.AddOrUpdate(
                    options.Region,
                    new HashSet<string> { key },
                    (_, keys) =>
                    {
                        lock (keys)
                        {
                            keys.Add(key);
                            return keys;
                        }
                    });

                memoryCacheEntryOptions.RegisterPostEvictionCallback((evictedKey, _, _, _) =>
                {
                    if (_regionKeys.TryGetValue(options.Region, out var keys))
                    {
                        lock (keys)
                        {
                            keys.Remove(key);
                        }
                    }
                });
            }

            _memoryCache.Set(key, value, memoryCacheEntryOptions);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);

            // Remove key from all regions
            foreach (var region in _regionKeys)
            {
                lock (region.Value)
                {
                    region.Value.Remove(key);
                }
            }
        }

        public void RemoveByPattern(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
                return;

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = _regionKeys.Values
                .SelectMany(x => x)
                .Where(k => regex.IsMatch(k))
                .ToList();

            foreach (var key in keysToRemove)
            {
                Remove(key);
            }
        }

        public void RemoveByRegion(string region)
        {
            if (string.IsNullOrEmpty(region))
                return;

            if (_regionKeys.TryGetValue(region, out var keys))
            {
                List<string> keysToRemove;
                lock (keys)
                {
                    keysToRemove = keys.ToList();
                }

                foreach (var key in keysToRemove)
                {
                    Remove(key);
                }

                _regionKeys.TryRemove(region, out _);
            }
        }

        public bool TryGet<T>(string key, out T value)
        {
            return _memoryCache.TryGetValue(key, out value);
        }

        public void Clear()
        {
            foreach (var region in _regionKeys.Keys.ToList())
            {
                RemoveByRegion(region);
            }
        }

        public bool Contains(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }

        public IEnumerable<string> GetKeys(string? region = null)
        {
            if (string.IsNullOrEmpty(region))
            {
                return _regionKeys.Values.SelectMany(x => x).Distinct();
            }

            return _regionKeys.TryGetValue(region, out var keys) ? keys.ToList() : Enumerable.Empty<string>();
        }
    }
}