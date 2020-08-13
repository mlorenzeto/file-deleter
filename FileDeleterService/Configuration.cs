using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FileDeleterService
{
    public class Configuration
    {
        private static IConfigurationRoot Config { get; set; }
        private static Configuration instance;

        public static bool RUN_AS_SERVICE = true;
        public static string PATH_TO_CONTENT_ROOT = Directory.GetCurrentDirectory();

        private Configuration(bool runAsService)
        {
            if (runAsService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                PATH_TO_CONTENT_ROOT = Path.GetDirectoryName(pathToExe);
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(PATH_TO_CONTENT_ROOT)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Config = builder.Build();
        }

        public static Configuration Instance()
        {
            if (instance == null)
            {
                instance = new Configuration(RUN_AS_SERVICE);
            }
            return instance;
        }

        public string Get(string key)
        {
            var value = Config.GetValue<string>(key);

            if (value == null)
            {
                throw new System.Exception($"Value for key: {key} not found");
            }
            return value;
        }
    }
}
