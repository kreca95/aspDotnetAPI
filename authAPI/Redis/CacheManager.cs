using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace authAPI.Redis
{
    public class CacheManager
    {
        RedisEndpoint _endPoint;

        public CacheManager()
        {
                _endPoint = new RedisEndpoint(RedisConfigurationManager.Config.Host, RedisConfigurationManager.Config.Port, RedisConfigurationManager.Config.Password, RedisConfigurationManager.Config.DatabaseID);
        }

    }
}