using System.IO;
using BplusDotNet;

namespace CacheAspect
{
    internal class BTreeCache : ICache
    {
        private static SerializedTree treeCache;
        private static string datafile;
        private static string treefile;

        public BTreeCache()
        {
            datafile = CacheService.DiskPath + "datafile";
            treefile = CacheService.DiskPath + "treefile";
            LoadCache();
        }

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


        public bool Contains(string key)
        {
            return treeCache.ContainsKey(key);
        }

        public void Delete(string key)
        {
            treeCache.RemoveKey(key);
            SaveCache();
        }

        public void Clear()
        {
            string key = treeCache.FirstKey();
            while (!string.IsNullOrWhiteSpace(key))
            {
                Delete(key);
                key = treeCache.FirstKey();
            }
        }

        ~BTreeCache()
        {
            CloseCache();
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

        public void CloseCache()
        {
            treeCache.Shutdown();
        }
    }
}