using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheAspect
{
    public interface ICache
    {
        object this[string key] { get; set; }

        bool Contains(string key);

        void Delete(string key);

        void Clear();
    }
}
