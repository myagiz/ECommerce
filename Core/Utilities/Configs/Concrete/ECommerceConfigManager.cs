using Core.Utilities.Configs.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Configs.Concrete
{
    public class ECommerceConfigManager : IECommerceConfigService
    {
        private readonly IConfiguration _configuration;

        public ECommerceConfigManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString(string connectionName)
        {
            return _configuration.GetConnectionString(connectionName);
        }
    }
}
