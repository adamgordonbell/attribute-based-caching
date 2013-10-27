using System;
using System.Text;
using System.Configuration;

namespace CacheAspect
{
    public static class CacheService
    {
        static CacheService()
        {
            InitDiskPath();
            InitTimeToLive();            
            InitCache();
        }

        private static void InitDiskPath()
        {
            DiskPath = ConfigurationManager.AppSettings["CacheAspect.DiskPath"];
        }

        private static void InitTimeToLive()
        {
            if (TimeToLive == TimeSpan.Parse("0:0:0:0"))
            {
                try
                {
                    TimeToLive = TimeSpan.Parse(ConfigurationManager.AppSettings["CacheAspect.TimeToLive"]);
                }
                catch
                {
                    TimeToLive = TimeSpan.MaxValue;
                }

            }
        }

        private static void InitCache()
        {
            if (Cache == null)
            {
                try
                {
                    Cache = (ICache)Activator.CreateInstance(Type.GetType(ConfigurationManager.AppSettings["CacheAspect.CacheType"]));

                }
                catch
                {
                    //if a cache is not configured, fall back on NoCache 
                    //this happens is useful for test cases
                    Cache = new NoCache();
                }
            }
        }

        public static ICache Cache;
        public static TimeSpan TimeToLive;
        public static string DiskPath;
    }
}
