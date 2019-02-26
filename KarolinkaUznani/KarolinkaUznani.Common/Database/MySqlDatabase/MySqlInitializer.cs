using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace KarolinkaUznani.Common.Database.MySqlDatabase
{
    public class MySqlInitializer : IDatabaseInitializer, IDisposable
    {
        /// <summary>
        /// Was the DB already initialized?
        /// </summary>
        private bool _initialized;

        /// <summary>
        /// Should the DB be seeded?
        /// </summary>
        private readonly bool _seed;

        /// <summary>
        /// Actual connection to the DB
        /// </summary>
        public readonly MySqlConnection Connection;

        /// <summary>
        /// Database seeder
        /// </summary>
        private readonly IDatabaseSeeder _seeder;

        /// <summary>MySqlInitializer
        /// Constructor
        /// </summary>
        /// <param name="database">Database connection</param>
        /// <param name="seeder">Database seeder</param>
        /// <param name="options">MySqlOptions object</param>
        public MySqlInitializer(MySqlConnection database, IDatabaseSeeder seeder, IOptions<MySqlOptions> options)
        {
            Connection = database;
            _seeder = seeder;
            _seed = options.Value.Seed;
        }

        /// <summary>
        /// Init DB async
        /// </summary>
        /// <returns>void</returns>
        public async Task InitializeAsync()
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;
            if (!_seed)
            {
                return;
            }

            await _seeder.SeedAsync();
        }

        /// <inheritdoc />
        /// <summary>
        /// End DB connection
        /// </summary>
        public void Dispose()
        {
            Connection.Close();
        }
    }
}