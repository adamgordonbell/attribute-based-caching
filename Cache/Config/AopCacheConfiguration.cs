using System.Configuration;

namespace CacheAspect.Config
{

    public class AopCacheConfiguration : ConfigurationSection
    {
        #region Constants

        private const string PathAttribute = "path";

        private const string TtlAttribute = "ttl";

        private const string TypeAttribute = "type";

        #endregion

        #region Public Properties

        [ConfigurationProperty(TypeAttribute, IsRequired = true, DefaultValue = "CacheAspect.MemoryCache")]
        public string CacheType
        {
            get { return (string) base[TypeAttribute]; }
        }

        [ConfigurationProperty(PathAttribute, IsRequired = false, DefaultValue = "")]
        public string Path
        {
            get { return (string) base[PathAttribute]; }
        }

        [ConfigurationProperty(TtlAttribute, IsRequired = false, DefaultValue = "01:00:00")]
        public string TimeToLive
        {
            get { return (string) base[TtlAttribute]; }
        }

        #endregion
    }
}