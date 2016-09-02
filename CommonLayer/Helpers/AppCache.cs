using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;


namespace WebAPI_with_Angular.Common
{
    public static class SPObjectCache
    {
        static readonly MemoryCache appCache = MemoryCache.Default;
        static Dictionary<string, IList<string>> changeListners = new Dictionary<string, IList<string>>();
        static object writeLock = new object();

        public static void RegisterForNotification(string spListName, IList<string> dependantCacheKeys)
        {
            try
            {

                lock (writeLock)
                {
                    List<string> cacheKeys;
                    if (changeListners.ContainsKey(spListName))
                    {
                        cacheKeys = changeListners[spListName].ToList();

                    }
                    else
                    {
                        cacheKeys = new List<string>();
                    }

                    cacheKeys.AddRange(dependantCacheKeys);

                    changeListners[spListName] = cacheKeys;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError(string.Format(@"Error Rehistering listner for {0} ", spListName), ex);
            }
        }

        public static void Add(string cacheKey, object value)
        {
            try
            {
                appCache[cacheKey] = value;
            }
            catch (Exception ex)
            {
                LogHelper.LogError(string.Format(@"Error adding {0} to cache", cacheKey), ex);
            }

        }

        public static object Get(string cacheKey)
        {
            try
            {
                return appCache[cacheKey];
            }
            catch (Exception ex)
            {
                LogHelper.LogError(string.Format(@"Error fetching {0} from cache", cacheKey), ex);
                throw;
            }

        }

        public static void Remove(string cacheKey)
        {
            try
            {
                appCache.Remove(cacheKey);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(string.Format(@"Error removing {0} from cache", cacheKey), ex);
            }

        }

        public static void OnSPListupdated(string spListName)
        {
            try
            {
                IList<string> dependantCacheKeys = changeListners[spListName];
                if (dependantCacheKeys != null)
                {
                    foreach (string cacheKey in dependantCacheKeys)
                    {
                        Remove(cacheKey);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError("Could not refresh cache", ex);
            }
        }
    }
}
