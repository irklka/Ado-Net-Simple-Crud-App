using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ado.Net.Config;
using Microsoft.Extensions.Configuration;

namespace Ado.Net
{
    /// <summary>
    /// Configure Database from appsettings.json
    /// </summary>
    public class DatabaseConfig : IDatabaseConfig
    {
        public string GetDbConnectionString(string connectionStringName)
        {
            var builder = new ConfigurationBuilder();

            // get configuration from appsettings.json
            builder.SetBasePath(Directory.GetCurrentDirectory());

            // get configuration from appsettings.json
            builder.AddJsonFile("appsettings.json");

            // create a configuration
            var config = builder.Build();

            // get connection string
            string connectionString = config.GetConnectionString(connectionStringName);

            return connectionString;
        }
    }
}
