using System;
using System.Threading.Tasks;

namespace CacheAspect
{
    public class MemoryCache : ICache
    {
        private const string PostSharpCache = "PostSharp.Cache";

        #region Fields

        private readonly System.Runtime.Caching.MemoryCache _cache = new System.Runtime.Caching.MemoryCache(PostSharpCache);

        #endregion

        #region Public Indexers

        public object this[string key]
        {
            get { return _cache.Get(key); }
            set { _cache.Set(key, value, DateTimeOffset.Now.AddYears(1)); }
        }

        #endregion

        #region Public Methods and Operators

        public bool Contains(string key)
        {
            return _cache.Contains(key);
        }

        public void Delete(string key)
        {
            _cache.Remove(key);
        }

        public void Clear()
        {
            Parallel.ForEach(_cache, pair => _cache.Remove(pair.Key));
        }

        #endregion
    }
}