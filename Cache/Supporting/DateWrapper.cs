using System;

namespace CacheAspect.Supporting
{
    #region

    

    #endregion

    [Serializable]
    public class DateWrapper<T>
    {
        #region Public Properties

        public T Object { get; set; }

        public DateTime Timestamp { get; set; }

        #endregion
    }
}