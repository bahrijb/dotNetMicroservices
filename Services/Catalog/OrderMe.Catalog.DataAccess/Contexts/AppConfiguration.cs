using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace OrderMe.Catalog.DataAccess.Contexts
{
    public static class AppConfiguration
    {
        public static IConfigurationRoot Get(string path = null, string environmentName = null)
        {
            if (string.IsNullOrEmpty(path))
                path = Directory.GetCurrentDirectory();

            if (string.IsNullOrEmpty(environmentName))
                environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appSettings.json", true);

            if (!string.IsNullOrWhiteSpace(environmentName))
                builder = builder.AddJsonFile($"appSettings.{environmentName}.json", true);

            builder = builder.AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
