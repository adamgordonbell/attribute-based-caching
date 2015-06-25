namespace CacheAspect
{
    //primarly used for unit tests
    public class NoCache : ICache
    {
        #region Public Indexers

        public object this[string key]
        {
            get { return null; }
            set { }
        }

        #endregion

        #region Public Methods and Operators

        public bool Contains(string key)
        {
            return false;
        }

        public void Delete(string key)
        {
        }

        public void Clear()
        {
        }

        #endregion
    }
}