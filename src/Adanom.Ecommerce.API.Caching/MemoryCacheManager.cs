using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Memory;

namespace Adanom.Ecommerce.API.Caching
{
    internal sealed class MemoryCacheManager : IMemoryCacheManager
    {
        #region Fields

        private readonly IMemoryCache _memoryCache;
        private readonly ConcurrentDictionary<string, HashSet<string>> _regionKeys;

        private static string NormalizeKey(string key) => key.ToUpperInvariant();

        #endregion

        #region Ctor

        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _regionKeys = new ConcurrentDictionary<string, HashSet<string>>();
        }

        #endregion

        #region MemoryCacheManager Members

        #region Get

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(NormalizeKey(key));
        }

        #endregion

        #region Set

        public void Set<T>(string key, T value, CacheOptions options)
        {
            var normalizedKey = NormalizeKey(key);
            var memoryCacheEntryOptions = new MemoryCacheEntryOptions();

            if (options.SlidingExpiration.HasValue)
            {
                memoryCacheEntryOptions.SlidingExpiration = options.SlidingExpiration.Value;
            }

            if (options.AbsoluteExpiration.HasValue)
            {
                memoryCacheEntryOptions.AbsoluteExpirationRelativeToNow = options.AbsoluteExpiration.Value;
            }

            if (!string.IsNullOrEmpty(options.Region))
            {
                _regionKeys.AddOrUpdate(
                    options.Region,
                    new HashSet<string> { normalizedKey },
                    (_, keys) =>
                    {
                        lock (keys)
                        {
                            keys.Add(normalizedKey);
                            return keys;
                        }
                    });

                memoryCacheEntryOptions.RegisterPostEvictionCallback((evictedKey, _, _, _) =>
                {
                    if (_regionKeys.TryGetValue(options.Region, out var keys))
                    {
                        lock (keys)
                        {
                            keys.Remove(normalizedKey);
                        }
                    }
                });
            }

            _memoryCache.Set(normalizedKey, value, memoryCacheEntryOptions);
        }

        #endregion

        #region Remove

        public void Remove(string key)
        {
            var normalizedKey = NormalizeKey(key);
            _memoryCache.Remove(normalizedKey);

            // Remove key from all regions
            foreach (var region in _regionKeys)
            {
                lock (region.Value)
                {
                    region.Value.Remove(normalizedKey);
                }
            }
        }

        #endregion

        #region RemoveByPattern

        public void RemoveByPattern(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                return;
            }

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

        #endregion

        #region RemoveByRegion

        public void RemoveByRegion(string region)
        {
            if (string.IsNullOrEmpty(region))
            {
                return;
            }

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

        #endregion

        #region TryGet

        public bool TryGet<T>(string key, out T value)
        {
            return _memoryCache.TryGetValue(NormalizeKey(key), out value);
        }

        #endregion

        #region Clear

        public void Clear()
        {
            foreach (var region in _regionKeys.Keys.ToList())
            {
                RemoveByRegion(region);
            }
        }

        #endregion

        #region Contains

        public bool Contains(string key)
        {
            return _memoryCache.TryGetValue(NormalizeKey(key), out _);
        }

        #endregion

        #region GetKeys

        public IEnumerable<string> GetKeys(string? region = null)
        {
            if (string.IsNullOrEmpty(region))
            {
                return _regionKeys.Values.SelectMany(x => x).Distinct();
            }

            return _regionKeys.TryGetValue(region, out var keys) ? keys.ToList() : Enumerable.Empty<string>();
        }

        #endregion

        #endregion
    }
}