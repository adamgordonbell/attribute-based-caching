using System;

namespace CacheAspect.Attributes
{
    #region

    

    #endregion

    [AttributeUsage(AttributeTargets.Parameter)]
    public class IgnoreAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class UsePropertyAttribute : Attribute
    {
        #region Fields

        private string _parameterValue = string.Empty;

        #endregion

        #region Constructors and Destructors

        public UsePropertyAttribute(string parameterValue)
        {
            _parameterValue = parameterValue;
        }

        #endregion
    }
}