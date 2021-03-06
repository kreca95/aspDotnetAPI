﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace authAPI.Redis
{
    public static class CacheFactory
    {


        public static ICacheProvider GetCacheProvider(string cache)
        {
            if (string.IsNullOrEmpty(cache))
            {
                return null;
            }
            else if (cache == "redis")
            {
                return new RedisCacheProvider();
            }
            else if (cache == "web")
            {
                return new WebCacheProvider();
            }
            return null;
        }
    }
}