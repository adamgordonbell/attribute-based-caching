using System.Collections.Generic;
using System.Runtime.Caching;

namespace CacheAspect
{
    public class SystemMemoryCache:ICache
    {
        public object this[string key]
        {
            get { return MemoryCache.Default[key]; }
            set { MemoryCache.Default[key] = value; }
        }

        public bool Contains(string key)
        {
            return MemoryCache.Default.Contains(key);
        }

        public void Delete(string key)
        {
            MemoryCache.Default.Remove(key);
        }

        public void Clear()
        {
            foreach (KeyValuePair<string, object> kvp in MemoryCache.Default)
            {
                MemoryCache.Default.Remove(kvp.Key);
            }
            
        }
    }
}
