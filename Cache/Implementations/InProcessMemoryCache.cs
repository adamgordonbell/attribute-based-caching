using System.Collections.Concurrent;

namespace CacheAspect
{
    public class InProcessMemoryCache : ICache
    {
        private readonly ConcurrentDictionary<string, object> _cache = new ConcurrentDictionary<string, object>();

        public object this[string key]
        {
            get { return _cache[key]; }
            set { _cache[key] = value; }
        }

        public bool Contains(string key)
        {
            return _cache.ContainsKey(key);
        }

        public void Delete(string key)
        {
            object o;
            _cache.TryRemove(key, out o);
        }

        public void Clear()
        {
            _cache.Clear();
        }
    }
}