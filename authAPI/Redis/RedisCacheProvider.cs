﻿using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace authAPI.Redis
{
    public class RedisCacheProvider : ICacheProvider
    {
        RedisEndpoint _endPoint;

        public RedisCacheProvider()
        {
            _endPoint = new RedisEndpoint("localhost",
               6379);
        }

        public void Set<T>(string key, T value)
        {
            this.Set(key, value, TimeSpan.Zero);
            CustomLogging.LogMessage(CustomLogging.TracingLevel.INFO, key + value +"from redis");
        }





        public void Set<T>(string key, T value, TimeSpan timeout)
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {
                client.As<T>().SetValue(key, value, timeout);
                CustomLogging.LogMessage(CustomLogging.TracingLevel.CACHE, key +"from redis");

            }
        }
        public T Get<T>(string key)
        {
            T result = default(T);

            using (RedisClient client = new RedisClient(_endPoint))
            {
                var wrapper = client.As<T>();

                result = wrapper.GetValue(key);
            }

            return result;
        }

        public bool Remove(string key)
        {
            bool removed = false;

            using (RedisClient client = new RedisClient(_endPoint))
            {
                removed = client.Remove(key);
            }

            return removed;
        }

        public bool IsInCache(string key)
        {
            bool isInCache = false;

            using (RedisClient client = new RedisClient(_endPoint))
            {
                isInCache = client.ContainsKey(key);
            }

            return isInCache;
        }

    }
}