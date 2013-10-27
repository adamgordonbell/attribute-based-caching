using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheAspect.Supporting
{
    [Serializable]
    public class DateWrapper<T>
    {
        public T Object
        {
            get;
            set;
        }
        public DateTime Timestamp
        {
            get;
            set;
        }
    }
}
