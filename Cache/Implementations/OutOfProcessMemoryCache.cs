using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ApplicationServer.Caching;

namespace CacheAspect
{
    public class OutOfProcessMemoryCache : ICache
    {
        public OutOfProcessMemoryCache()
        {
            var configuration = new DataCacheFactoryConfiguration();
            var factory = new DataCacheFactory(configuration);
            cache = factory.GetCache(CacheName);
        }
        
        public object this[string key]
        {
            get
            {
                return cache[key]; 
            }
            set
            {
                cache[key] = value;
            }
        }


        public bool Contains(string key)
        {
            //App Fabric Cache has no Contains method
            return cache[key] != null;
        }


        public void Delete(string key)
        {
            cache.Remove(key);
        }

        public static String CacheName = "CacheAttribute";
        private static DataCache cache;
    }
}
