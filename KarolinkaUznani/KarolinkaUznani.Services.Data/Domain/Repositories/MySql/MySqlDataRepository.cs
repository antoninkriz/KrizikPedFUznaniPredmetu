using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using KarolinkaUznani.Common.Database;
using KarolinkaUznani.Services.Data.Domain.Models;
using MySql.Data.MySqlClient;

namespace KarolinkaUznani.Services.Data.Domain.Repositories.MySql
{
    /// <inheritdoc cref="IDataRepository" />
    /// <summary>
    /// Handling database stuff related to the data
    /// </summary>
    public class MySqlDataRepository : BaseMySqlRepository, IDataRepository
    {
        public MySqlDataRepository(MySqlConnection dbConnection) : base(dbConnection)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Get list of Katedra for specified parameters
        /// </summary>
        /// <param name="searchText">Text to search for</param>
        /// <returns></returns>
        public async Task<List<KatedraDbModel>> GetKatedraAsync(string searchText)
        {
            using (var command = Command("sp_SearchKatedry", new List<Param>
            {
                new Param("p_search", MySqlDbType.VarChar, 64, searchText)
            }))
            {
                await OpenConnectionAsync();

                var collection = new List<KatedraDbModel>();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var i = 0;

                        collection.Add(new KatedraDbModel(
                            id: new Guid(await reader.GetFieldValueAsync<byte[]>(i++)),
                            name: await reader.GetFieldValueAsync<string>(i)
                        ));
                    }
                }

                return collection;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Get list of DruhStudia for specified parameters
        /// </summary>
        /// <param name="katedraId">GUID of Katedra to search DruhStudia for</param>
        /// <param name="searchText">Text to search for</param>
        /// <returns></returns>
        public async Task<List<DruhStudiaDbModel>> GetDruhStudiaAsync(Guid katedraId, string searchText)
        {
            using (var command = Command("sp_SearchDruhyStudia", new List<Param>
            {
                new Param("p_search", MySqlDbType.VarChar, 64, searchText),
                new Param("p_katedraId", MySqlDbType.Binary, 16, katedraId.ToByteArray())
            }))
            {
                await OpenConnectionAsync();

                var collection = new List<DruhStudiaDbModel>();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var i = 0;

                        collection.Add(new DruhStudiaDbModel(
                            id: new Guid(await reader.GetFieldValueAsync<byte[]>(i++)),
                            code: await reader.GetFieldValueAsync<string>(i++),
                            name: await reader.GetFieldValueAsync<string>(i)
                        ));
                    }
                }

                return collection;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Get list of Obor for specified parameters
        /// </summary>
        /// <param name="katedraId">GUID of Katedra to search Obor for</param>
        /// <param name="druhStudiaId">GUID of DruhStudia to search Obor for</param>
        /// <param name="searchText">Text to search for</param>
        /// <returns></returns>
        public async Task<List<OborDbModel>> GetOborAsync(Guid katedraId, Guid druhStudiaId, string searchText)
        {
            using (var command = Command("sp_SearchObory", new List<Param>
            {
                new Param("p_search", MySqlDbType.VarChar, 64, searchText),
                new Param("p_katedraId", MySqlDbType.Binary, 16, katedraId.ToByteArray()),
                new Param("p_druhStudiaId", MySqlDbType.Binary, 16, druhStudiaId.ToByteArray())
            }))
            {
                await OpenConnectionAsync();

                var collection = new List<OborDbModel>();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var i = 0;

                        var a = new Guid(await reader.GetFieldValueAsync<byte[]>(i++));
                        var b = await reader.GetFieldValueAsync<string>(i++);
                        var c = await reader.GetFieldValueAsync<string>(i++);
                        var d = ConvertFromDbVal<string>(await reader.GetFieldValueAsync<object>(i++));  
                        var e = ConvertFromDbVal<int?>(await reader.GetFieldValueAsync<object>(i++));
                        var f = ConvertFromDbVal<int?>(await reader.GetFieldValueAsync<object>(i++));
                        var g = ConvertFromDbVal<bool>(await reader.GetFieldValueAsync<object>(i));
                        
                        collection.Add(new OborDbModel(
                            a,b,c,d,e,f,g
                        ));
                    }
                }

                return collection;
            }
        }

        private static T ConvertFromDbVal<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value || obj is DBNull)
                return default;

            var t = typeof(T);

            if (!t.IsGenericType || t.GetGenericTypeDefinition() != typeof(Nullable<>))
                return (T) Convert.ChangeType(obj, t);
            
            t = Nullable.GetUnderlyingType(t);

            return (T) Convert.ChangeType(obj, t);
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Get list of Predmet for specified parameters
        /// </summary>
        /// <param name="oborId">GUID of Obor to search Predmet for</param>
        /// <param name="searchText">Text to search for</param>
        /// <returns></returns>
        public async Task<List<PredmetDbModel>> GetPredmetAsync(Guid oborId, string searchText)
        {
            using (var command = Command("sp_SearchPredmety", new List<Param>
            {
                new Param("p_search", MySqlDbType.VarChar, 64, searchText),
                new Param("p_oborId", MySqlDbType.Binary, 16, oborId.ToByteArray()),
            }))
            {
                await OpenConnectionAsync();

                var collection = new List<PredmetDbModel>();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var i = 0;

                        collection.Add(new PredmetDbModel(
                            id: new Guid(await reader.GetFieldValueAsync<byte[]>(i++)),
                            code: await reader.GetFieldValueAsync<string>(i++),
                            name: await reader.GetFieldValueAsync<string>(i++),
                            kredity: await reader.GetFieldValueAsync<int>(i++),
                            zkouska: GetZkouska(
                                ConvertFromDbVal<bool>(await reader.GetFieldValueAsync<object>(i++)),
                                ConvertFromDbVal<bool>(await reader.GetFieldValueAsync<object>(i++)),
                                ConvertFromDbVal<bool>(await reader.GetFieldValueAsync<object>(i++)),
                                ConvertFromDbVal<bool>(await reader.GetFieldValueAsync<object>(i))
                            )
                        ));
                    }
                }

                return collection;
            }
        }

        /// <summary>
        /// Builds string according to types of zkouska for given Predmet
        /// </summary>
        /// <param name="ukZ">Zapocet</param>
        /// <param name="ukKz">Klasifikovany zapocet</param>
        /// <param name="ukZk">Zkouska</param>
        /// <param name="ukKlp">Klauzurni prace</param>
        /// <returns></returns>
        private static string GetZkouska(bool ukZ, bool ukKz, bool ukZk, bool ukKlp)
        {
            var uk = new List<string>();

            if (ukZ)
                uk.Add("Z");
            if (ukKz)
                uk.Add("Kz");
            if (ukZk)
                uk.Add("Zk");
            if (ukKlp)
                uk.Add("Klp");

            return string.Join(" + ", uk);
        }
    }
}