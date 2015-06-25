using System.Collections.Concurrent;

namespace CacheAspect
{
    #region

    

    #endregion

    public class InProcessMemoryCache : ICache
    {
        #region Fields

        private readonly ConcurrentDictionary<string, object> _cache = new ConcurrentDictionary<string, object>();

        #endregion

        #region Public Indexers

        public object this[string key]
        {
            get { return _cache[key]; }
            set { _cache[key] = value; }
        }

        #endregion

        #region Public Methods and Operators

        public bool Contains(string key)
        {
            return _cache.ContainsKey(key);
        }

        public void Delete(string key)
        {
            object keyOut;
            _cache.TryRemove(key, out keyOut);
            keyOut = null;
        }

        public void Clear()
        {
            _cache.Clear();
        }

        #endregion
    }
}