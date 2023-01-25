using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuviaGS.Common.Helper
{
    public class AppSettingsHelper
    {
        private static IConfiguration _config;
        private static IWebHostEnvironment _environment;
        public static void AppSettingsConfigure(IConfiguration config, IWebHostEnvironment environment)
        {
            _config = config;
            _environment = environment;
        }

        public static string Settings(string key)
        {
            return _config.GetSection(key).Value;
        }

        public static IWebHostEnvironment GetWebHostEnvironment()
        {
            return _environment;
        }

    }
}
