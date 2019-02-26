using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KarolinkaUznani.Common.Responses.Data;

namespace KarolinkaUznani.Services.Data.Services
{
    /// <summary>
    /// Interface for the DataService
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Get list of Katedra for specified parameters
        /// </summary>
        /// <param name="searchText">Text to search for</param>
        /// <returns>List of Katedra that are valid for given parameters</returns>
        Task<List<Katedra>> GetKatedraAsync(string searchText);

        /// <summary>
        /// Get list of DruhStudia for specified parameters
        /// </summary>
        /// <param name="katedraId">GUID of Katedra to get the DruhStudia for</param>
        /// <param name="searchText">Text to search for</param>
        /// <returns>List of DruhStudia that are valid for given parameters</returns>
        Task<List<DruhStudia>> GetDruhStudiaAsync(Guid katedraId, string searchText);

        /// <summary>
        /// Get list of Obor for specified parameters
        /// </summary>
        /// <param name="katedraId">GUID of Katedra to get the Obor for</param>
        /// <param name="druhStudiaId">GUID of DruhStudia to get the Obor for</param>
        /// <param name="searchText">Text to search for</param>
        /// <returns>List of Obor that are valid for given parameters</returns>
        Task<List<Obor>> GetOborAsync(Guid katedraId, Guid druhStudiaId, string searchText);

        /// <summary>
        /// Get list of Predmet for specified parameters
        /// </summary>
        /// <param name="oborId">GUID of Obor to get the Predmet for</param>
        /// <param name="searchText">Text to search for</param>
        /// <returns>List of Predmet that are valid for given parameters</returns>
        Task<List<Predmet>> GetPredmetAsync(Guid oborId, string searchText);
    }
}