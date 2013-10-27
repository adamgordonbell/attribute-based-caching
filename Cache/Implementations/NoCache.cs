using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheAspect
{
    //primarly used for unit tests
    public class NoCache : ICache
    {
        public object this[string key]
        {
            get
            {
                return null; 
            }
            set
            {
                ;
            }
        }

        public bool Contains(string key)
        {
            return false;
        }

        public void Delete(string key)
        {
            ;
        }
    }
}
