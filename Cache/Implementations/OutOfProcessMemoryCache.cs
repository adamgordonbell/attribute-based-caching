using Microsoft.ApplicationServer.Caching;

namespace CacheAspect
{
    #region

    

    #endregion

    public class OutOfProcessMemoryCache : ICache
    {
        #region Constructors and Destructors

        public OutOfProcessMemoryCache()
        {
            var configuration = new DataCacheFactoryConfiguration();
            var factory = new DataCacheFactory(configuration);
            _cache = factory.GetCache(CacheName);
        }

        #endregion

        #region Public Indexers

        public object this[string key]
        {
            get { return _cache[key]; }
            set { _cache[key] = value; }
        }

        #endregion

        #region Static Fields

        public static string CacheName = "CacheAttribute";

        private static DataCache _cache;

        #endregion

        #region Public Methods and Operators

        public bool Contains(string key)
        {
            //App Fabric Cache has no Contains method
            return _cache[key] != null;
        }

        public void Delete(string key)
        {
            _cache.Remove(key);
        }

        public void Clear()
        {
            foreach (var region in _cache.GetSystemRegions())
            {
                _cache.ClearRegion(region);
            }
        }

        #endregion
    }
}