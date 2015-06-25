namespace CacheAspect
{
    public interface ICache
    {
        #region Public Indexers

        object this[string key] { get; set; }

        #endregion

        #region Public Methods and Operators

        bool Contains(string key);

        void Delete(string key);

        void Clear();

        #endregion
    }
}