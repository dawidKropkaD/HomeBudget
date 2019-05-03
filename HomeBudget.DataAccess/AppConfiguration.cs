using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.DataAccess
{
    public class AppConfiguration
    {
        IConfigurationRoot configurationRoot;

        public AppConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            configurationRoot = configurationBuilder.Build();
        }


        public string ConnectionString
        {
            get => configurationRoot.GetSection("ConnectionString").GetSection("HomeBudget").Value;
        }
    }
}
