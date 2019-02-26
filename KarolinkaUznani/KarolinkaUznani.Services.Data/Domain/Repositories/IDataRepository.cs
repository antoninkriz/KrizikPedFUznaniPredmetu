using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KarolinkaUznani.Common.Database;
using KarolinkaUznani.Services.Data.Domain.Models;

namespace KarolinkaUznani.Services.Data.Domain.Repositories
{
    /// <summary>
    /// Interface for the DataRepository
    /// </summary>
    public interface IDataRepository
    {
        /// <summary>
        /// Get list of Katedra for specified parameters
        /// </summary>
        /// <param name="searchText">Text to search for</param>
        /// <returns></returns>
        Task<List<KatedraDbModel>> GetKatedraAsync(string searchText);

        /// <summary>
        /// Get list of DruhStudia for specified parameters
        /// </summary>
        /// <param name="katedraId">GUID of Katedra to search DruhStudia for</param>
        /// <param name="searchText">Text to search for</param>
        /// <returns></returns>
        Task<List<DruhStudiaDbModel>> GetDruhStudiaAsync(Guid katedraId, string searchText);
        
        /// <summary>
        /// Get list of Obor for specified parameters
        /// </summary>
        /// <param name="katedraId">GUID of Katedra to search Obor for</param>
        /// <param name="druhStudiaId">GUID of DruhStudia to search Obor for</param>
        /// <param name="searchText">Text to search for</param>
        /// <returns></returns>
        Task<List<OborDbModel>> GetOborAsync(Guid katedraId, Guid druhStudiaId, string searchText);
        
        /// <summary>
        /// Get list of Predmet for specified parameters
        /// </summary>
        /// <param name="oborId">GUID of Obor to search Predmet for</param>
        /// <param name="searchText">Text to search for</param>
        /// <returns></returns>
        Task<List<PredmetDbModel>> GetPredmetAsync(Guid oborId, string searchText);
    }
}