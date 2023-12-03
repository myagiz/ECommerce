using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Configs
{
    public class ECommerceConfigManager
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string GetConnectionString(string connectionName)
        {
            if (_configuration == null)
            {
                throw new InvalidOperationException("ECommerceConfigManager has not been initialized. Call Initialize method first.");
            }

            return _configuration.GetConnectionString(connectionName);
        }
    }

}
