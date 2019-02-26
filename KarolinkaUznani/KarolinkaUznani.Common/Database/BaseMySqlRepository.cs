using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace KarolinkaUznani.Common.Database
{
    public abstract class BaseMySqlRepository
    {
        private readonly MySqlConnection _dbConnection;

        protected BaseMySqlRepository(MySqlConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// Opens connection to the DB when needed
        /// </summary>
        /// <returns></returns>
        protected async Task OpenConnectionAsync()
        {
            if (_dbConnection.State == ConnectionState.Broken)
                _dbConnection.Close();

            if (_dbConnection.State == ConnectionState.Closed)
                await _dbConnection.OpenAsync();
        }

        /// <summary>
        /// Creates a MySqlCommand without needing so much boilerplate
        /// </summary>
        /// <param name="storedProcedure">Name of the stored procedure</param>
        /// <param name="parameters">List of SQL procedure parameters</param>
        /// <returns></returns>
        protected MySqlCommand Command(string storedProcedure, IEnumerable<Param> parameters = null)
        {
            var command = new MySqlCommand
            {
                Connection = _dbConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = storedProcedure
            };

            var sqlParams = parameters?.Select(obj =>
                                obj.Size == null
                                    ? new MySqlParameter(obj.Name, obj.DbType)
                                    {
                                        Value = obj.Value,
                                        IsNullable = true
                                    }
                                    : new MySqlParameter(obj.Name, obj.DbType, (int) obj.Size)
                                    {
                                        Value = obj.Value,
                                        IsNullable = true
                                    }
                            ) ?? new List<MySqlParameter>();

            command.Parameters.AddRange(sqlParams.ToArray());

            return command;
        }

        /// <summary>
        /// Custom parameter object that makes code looks cleaner
        /// </summary>
        protected class Param
        {
            public string Name { get; }
            public MySqlDbType DbType { get; }
            public object Value { get; }
            public int? Size { get; }

            public Param(string name, MySqlDbType dbType, int? size, object value)
            {
                Name = name;
                DbType = dbType;
                Value = value;
                Size = size;
            }
        }
    }
}