using System;
using System.Configuration;
using CacheAspect.Config;

namespace CacheAspect
{
    #region

    

    #endregion

    public static class CacheService
    {
        #region Constructors and Destructors

        static CacheService()
        {
            Configuration = ConfigurationManager.GetSection("aopCacheConfiguration") as AopCacheConfiguration ?? new AopCacheConfiguration();
            DiskPath = Configuration.Path;
            TimeSpan.TryParse(Configuration.TimeToLive, out TimeToLive);
            InitCache();
        }

        #endregion

        #region Methods

        private static void InitCache()
        {
            if (Cache == null)
            {
                try
                {
// ReSharper disable once AssignNullToNotNullAttribute
                    Cache = (ICache) Activator.CreateInstance(Type.GetType(Configuration.CacheType));
                }
                catch
                {
                    //if a cache is not configured, fall back on NoCache 
                    //this happens is useful for test cases
                    Cache = new NoCache();
                }
            }
        }

        #endregion

        #region Static Fields

        public static ICache Cache;

        public static AopCacheConfiguration Configuration;

        public static string DiskPath;

        public static TimeSpan TimeToLive;

        #endregion
    }
}