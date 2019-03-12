using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KarolinkaUznani.Common.Database;
using KarolinkaUznani.Services.Data.Domain.Models;
using MySql.Data.MySqlClient;

namespace KarolinkaUznani.Services.Data.Domain.Repositories.MySql
{
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
            using (var command = Command("sp_KatedraBySearch", new List<Param>
            {
                new Param("p_searchText", MySqlDbType.VarChar, 255, searchText)
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
            using (var command = Command("sp_DruhStudiaBySearch", new List<Param>
            {
                new Param("p_katedra_id", MySqlDbType.Binary, 16, katedraId.ToByteArray()),
                new Param("p_searchText", MySqlDbType.VarChar, 255, searchText)
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
            using (var command = Command("sp_OborBySearch", new List<Param>
            {
                new Param("p_katedra_id", MySqlDbType.Binary, 16, katedraId.ToByteArray()),
                new Param("p_druhStudia_id", MySqlDbType.Binary, 16, druhStudiaId.ToByteArray()),
                new Param("p_searchText", MySqlDbType.VarChar, 255, searchText)
            }))
            {
                await OpenConnectionAsync();

                var collection = new List<OborDbModel>();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var i = 0;
                        
                        collection.Add(new OborDbModel(
                            id: new Guid(await reader.GetFieldValueAsync<byte[]>(i++)),
                            code: await reader.GetFieldValueAsync<string>(i++),
                            name: await reader.GetFieldValueAsync<string>(i++),
                            specification: await  reader.GetFieldValueAsync<string>(i++),
                            yearFrom: await reader.GetFieldValueAsync<int?>(i++),
                            yearTo: await reader.GetFieldValueAsync<int?>(i)
                        ));
                    }
                }

                return collection;
            }
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
            using (var command = Command("sp_PredmetBySearch", new List<Param>
            {
                new Param("p_obor_id", MySqlDbType.Binary, 16, oborId.ToByteArray()),
                new Param("p_searchText", MySqlDbType.VarChar, 255, searchText)
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
                            zkouska: await reader.GetFieldValueAsync<string>(i++),
                            kredity: await reader.GetFieldValueAsync<int>(i)
                        ));
                    }
                }

                return collection;
            }
        }
    }
}