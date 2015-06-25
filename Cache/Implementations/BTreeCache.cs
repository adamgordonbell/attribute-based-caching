using System.IO;
using BplusDotNet;

namespace CacheAspect
{

    #region

    #endregion

    public class BTreeCache : ICache
    {
        #region Public Indexers

        public object this[string key]
        {
            get
            {
                if (treeCache.ContainsKey(key))
                {
                    return treeCache[key];
                }
                return null;
            }
            set
            {
                treeCache[key] = value;
                SaveCache();
            }
        }

        #endregion

        #region Static Fields

        private static string datafile;

        private static SerializedTree treeCache;

        private static string treefile;

        #endregion

        #region Constructors and Destructors

        public BTreeCache()
        {
            datafile = CacheService.DiskPath + "datafile";
            treefile = CacheService.DiskPath + "treefile";
            LoadCache();
        }

        ~BTreeCache()
        {
            CloseCache();
        }

        #endregion

        #region Public Methods and Operators

        public bool Contains(string key)
        {
            return treeCache.ContainsKey(key);
        }

        public void Delete(string key)
        {
            treeCache.RemoveKey(key);
            SaveCache();
        }

        public void CloseCache()
        {
            treeCache.Shutdown();
        }

        public void LoadCache()
        {
            if (treeCache == null)
            {
                if (File.Exists(treefile) && File.Exists(datafile))
                {
                    treeCache = new SerializedTree(hBplusTreeBytes.ReOpen(treefile, datafile));
                }
                else
                {
                    treeCache = new SerializedTree(hBplusTreeBytes.Initialize(treefile, datafile, 500));
                }
                treeCache.SetFootPrintLimit(10);
            }
        }

        public void SaveCache()
        {
            if (treeCache != null)
            {
                treeCache.Commit();
            }
        }

        public void Clear()
        {
            var key = treeCache.FirstKey();
            while (!string.IsNullOrWhiteSpace(key))
            {
                Delete(key);
                key = treeCache.FirstKey();
            }
        }

        #endregion
    }
}