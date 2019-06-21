using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Caching;

namespace authAPI.Redis
{
    public class WebCacheProvider : ICacheProvider
    {

        public T Get<T>(string key)
        {
            var cache = (T)HttpRuntime.Cache.Get(key);
            CustomLogging.LogMessage(CustomLogging.TracingLevel.CACHE, key + "from webCache");

            return cache;
        }

        public bool IsInCache(string key)
        {
            var cache = HttpRuntime.Cache.Get(key);
            if (cache != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Remove(string key)
        {

            var cache = (bool)HttpRuntime.Cache.Remove(key);
            return cache;

        }

        public void Set<T>(string key, T value)
        {
            HttpRuntime.Cache.Insert(key, value);
            CustomLogging.LogMessage(CustomLogging.TracingLevel.CACHE, key + "from webCache");

        }

        public void Set<T>(string key, T value, TimeSpan timeout)
        {
            HttpRuntime.Cache.Insert(key, value, null,Cache.NoAbsoluteExpiration, timeout);
        }
    }
}