using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KarolinkaUznani.Common.Exceptions;
using KarolinkaUznani.Common.Responses.Data;
using KarolinkaUznani.Services.Data.Domain.Repositories;

namespace KarolinkaUznani.Services.Data.Services
{
    /// <inheritdoc />
    /// <summary>
    /// Service related to all Data requests
    /// </summary>
    public class DataService : IDataService
    {
        private readonly IDataRepository _dataRepository;

        public DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        /// <inheritdoc />
        /// <summary>
        /// Get list of Katedra for specified parameters
        /// </summary>
        /// <param name="searchText">Text to search for</param>
        /// <returns>List of Katedra that are valid for given parameters</returns>
        public async Task<List<Katedra>> GetKatedraAsync(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return new List<Katedra>();

            var result = await _dataRepository.GetKatedraAsync(searchText);

            return result.Select(x => new Katedra(x.Id, x.Name)).ToList();
        }

        /// <inheritdoc />
        /// <summary>
        /// Get list of DruhStudia for specified parameters
        /// </summary>
        /// <param name="katedraId">GUID of Katedra to get the DruhStudia for</param>
        /// <param name="searchText">Text to search for</param>
        /// <returns>List of DruhStudia that are valid for given parameters</returns>
        public async Task<List<DruhStudia>> GetDruhStudiaAsync(Guid katedraId, string searchText)
        {   
            if (katedraId == Guid.Empty)
                throw new KarolinkaException(KarolinkaException.ExceptionType.InvalidInput);

            if (string.IsNullOrWhiteSpace(searchText))
                return new List<DruhStudia>();
            
            var result = await _dataRepository.GetDruhStudiaAsync(katedraId, searchText);

            return result.Select(x => new DruhStudia(x.Id, x.Code, x.Name)).ToList();
        }

        /// <inheritdoc />
        /// <summary>
        /// Get list of Obor for specified parameters
        /// </summary>
        /// <param name="katedraId">GUID of Katedra to get the Obor for</param>
        /// <param name="druhStudiaId">GUID of DruhStudia to get the Obor for</param>
        /// <param name="searchText">Text to search for</param>
        /// <returns>List of Obor that are valid for given parameters</returns>
        public async Task<List<Obor>> GetOborAsync(Guid katedraId, Guid druhStudiaId, string searchText)
        {
            if (katedraId == Guid.Empty
                || druhStudiaId == Guid.Empty)
                throw new KarolinkaException(KarolinkaException.ExceptionType.InvalidInput);

            if (string.IsNullOrWhiteSpace(searchText))
                return new List<Obor>();

            var result = await _dataRepository.GetOborAsync(katedraId, druhStudiaId, searchText);

            return result.Select(x => new Obor(x.Id, x.Name, x.Code, x.Specification, x.YearFrom, x.YearTo, x.StudyForm)).ToList();
        }

        /// <inheritdoc />
        /// <summary>
        /// Get list of Predmet for specified parameters
        /// </summary>
        /// <param name="oborId">GUID of Obor to get the Predmet for</param>
        /// <param name="searchText">Text to search for</param>
        /// <returns>List of Predmet that are valid for given parameters</returns>
        public async Task<List<Predmet>> GetPredmetAsync(Guid oborId, string searchText)
        {
            if (oborId == Guid.Empty)
                throw new KarolinkaException(KarolinkaException.ExceptionType.InvalidInput);

            if (string.IsNullOrWhiteSpace(searchText))
                return new List<Predmet>();
            
            var result = await _dataRepository.GetPredmetAsync(oborId, searchText);

            return result.Select(x => new Predmet(x.Id, x.Code, x.Name, x.Zkouska, x.Kredity)).ToList();
        }
    }
}