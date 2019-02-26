using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace KarolinkaUznani.Common.Database.MySqlDatabase
{
    /// <summary>
    /// Seeds the DB
    /// </summary>
    public class MySqlSeeder : IDatabaseSeeder
    {
        /// <summary>
        /// Actual connection to the DB
        /// </summary>
        private readonly MySqlConnection _connection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="database">DbConnection to the DB</param>
        public MySqlSeeder(MySqlConnection database)
        {
            _connection = database;
        }

        /// <summary>
        /// Seeding methond, here can be run some first-run queries
        /// </summary>
        /// <returns></returns>
        public async Task SeedAsync()
        {
            // Calls to seeding methods here
            await CustomSeedAsync();
        }

        /// <summary>
        /// Seeding method
        /// </summary>
        /// <returns>void</returns>
        protected virtual async Task CustomSeedAsync()
        {
            await Task.CompletedTask;
        }
    }
}