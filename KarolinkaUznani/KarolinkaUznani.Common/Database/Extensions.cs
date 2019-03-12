using System;
using System.Collections.Generic;
using KarolinkaUznani.Common.Database.MySqlDatabase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace KarolinkaUznani.Common.Database
{
    public static class Extensions
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration databaseConfiguration, Dictionary<string, List<(Type, Type)>> repositories)
        {
            var dbConfig = databaseConfiguration.GetSection("database");
            var databaseToUse = dbConfig.GetValue<string>("use");

            if (databaseToUse == null || !repositories.ContainsKey(databaseToUse))
                throw new Exception($"Repositories for database type '{databaseToUse ?? "null"}' are not implemented");
            
            switch (databaseToUse)
            {
                case "mysql":
                    services.AddMySqlDb(dbConfig.GetSection("types").GetSection(databaseToUse));
                    break;
                default:
                    throw new Exception($"Database type '{databaseToUse ?? "null"}' is not implemented. Check appconfig.json");
            }
            
            repositories[databaseToUse].ForEach(x => services.AddScoped(x.Item1, x.Item2));
        }
        
        /// <summary>
        /// Extension that adds MySql connection
        /// </summary>
        /// <param name="services">ServicesCollection</param>
        /// <param name="configuration">Appsettings config</param>
        private static void AddMySqlDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MySqlOptions>(configuration);
            services.AddTransient(c =>
            {
                var options = c.GetService<IOptions<MySqlOptions>>();

                var connectionStringBuilder = new MySqlConnectionStringBuilder()
                {
                    Server = options.Value.Server,
                    Port = options.Value.Port,
                    UserID = options.Value.User,
                    Password = options.Value.Password,
                    Database = options.Value.Database,
                };
                return new MySqlConnection(connectionStringBuilder.ToString());
            });

            services.AddScoped<IDatabaseInitializer, MySqlInitializer>();
            services.AddScoped<IDatabaseSeeder, MySqlSeeder>();
        }
    }
}