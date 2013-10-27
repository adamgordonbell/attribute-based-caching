using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheAspect.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class IgnoreAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class UsePropertyAttribute : Attribute
    {
        public UsePropertyAttribute(String parameterValue)
        {
            _parameterValue = parameterValue;
        }

        string _parameterValue = string.Empty;
    }
}
