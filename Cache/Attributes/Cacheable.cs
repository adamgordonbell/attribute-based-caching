using System;
using System.Reflection;
using CacheAspect.Supporting;
using PostSharp.Aspects;

namespace CacheAspect.Attributes
{

    #region

    #endregion

    public static partial class Cache
    {
        [Serializable]
        public class Cacheable : OnMethodBoundaryAspect
        {
            #region Fields

            private readonly Lazy<KeyBuilder> _keyBuilder = new Lazy<KeyBuilder>(() => new KeyBuilder());

            #endregion

            #region Public Properties

            public KeyBuilder KeyBuilder
            {
                get { return _keyBuilder.Value; }
            }

            #endregion

            #region Methods

            private bool IsTooOld(DateTime time)
            {
                if (KeyBuilder.Settings == CacheSettings.IgnoreTTL)
                {
                    return false;
                }
                return DateTime.UtcNow - time > CacheService.TimeToLive;
            }

            #endregion

            #region Constructors and Destructors

            /// <summary>Parametrized cache constructor allowing to specify groups of cache and cache settings</summary>
            /// <param name="groupName"></param>
            /// <param name="settings"></param>
            /// <param name="parameterProperty"></param>
            public Cacheable(string groupName, CacheSettings settings, string parameterProperty)
            {
                KeyBuilder.GroupName = groupName;
                KeyBuilder.Settings = settings;
                KeyBuilder.ParameterProperty = parameterProperty;
            }

            /// <summary>Parametrized cache constructor allowing to specify groups of cache and cache settings</summary>
            /// <param name="groupName">Name of group which should contain the cached value</param>
            /// <param name="settings">TBD</param>
            public Cacheable(string groupName, CacheSettings settings) : this(groupName, settings, string.Empty)
            {
            }

            /// <summary>Parametrized cache constructor allowing to specify groups of cache</summary>
            /// <param name="groupName">Name of group which should contain the cached value</param>
            public Cacheable(string groupName) : this(groupName, CacheSettings.Default)
            {
            }

            /// <summary>Default cache constructor</summary>
            public Cacheable() : this(string.Empty)
            {
            }

            #endregion

            #region Public Methods and Operators

            public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
            {
                KeyBuilder.MethodParameters = method.GetParameters();
                KeyBuilder.MethodName = string.Format("{0}.{1}", method.DeclaringType != null ? method.DeclaringType.FullName : string.Empty, method.Name);
            }

            // This method is executed before the execution of target methods of this aspect.
            public override sealed void OnEntry(MethodExecutionArgs args)
            {
                // Compute the cache key.
                var cacheKey = KeyBuilder.BuildCacheKey(args.Instance, args.Arguments);

                // Fetch the value from the cache.
                var cache = CacheService.Cache;
                var value = (DateWrapper<object>) (cache.Contains(cacheKey) ? cache[cacheKey] : null);
                if (value != null && !IsTooOld(value.Timestamp))
                {
                    // The value was found in cache. Don't execute the method. Return immediately.
                    args.ReturnValue = value.Object;
                    args.FlowBehavior = FlowBehavior.Return;
                }
                else
                {
                    // The value was NOT found in cache. Continue with method execution, but store
                    // the cache key so that we don't have to compute it in OnSuccess.
                    args.MethodExecutionTag = cacheKey;
                }
            }

            // This method is executed upon successful completion of target methods of this aspect.
            public override sealed void OnSuccess(MethodExecutionArgs args)
            {
                var cacheKey = (string) args.MethodExecutionTag;
                CacheService.Cache[cacheKey] = new DateWrapper<object> {Object = args.ReturnValue, Timestamp = DateTime.UtcNow};
            }

            #endregion
        }
    }
}