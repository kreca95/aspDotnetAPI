using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace authAPI.Redis
{
    public class RedisConfigurationManager
    {
        #region Constants

        private const string SectionName = "RedisConfiguration";

        public static RedisConfigurationSection Config
        {
            get
            {
                return (RedisConfigurationSection)ConfigurationManager.GetSection(SectionName);
            }
        }

        #endregion
    }
}