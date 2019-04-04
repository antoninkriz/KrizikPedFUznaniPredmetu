using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KarolinkaUznani.Common.Exceptions;
using KarolinkaUznani.Common.Requests;
using KarolinkaUznani.Common.Requests.Data;
using KarolinkaUznani.Common.Responses.Data;
using KarolinkaUznani.Services.Data.Services;
using Microsoft.Extensions.Logging;

namespace KarolinkaUznani.Services.Data.Handlers
{
    /// <summary>
    /// Handler for DruhStudia requet
    /// </summary>
    public class RequestDruhStudiaHandler : IRequestHandler<DruhStudiaRequest, DruhStudiaResponse>
    {
        private readonly IDataService _dataService;
        private readonly ILogger _logger;

        public RequestDruhStudiaHandler(IDataService dataService, ILogger<RequestDruhStudiaHandler> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }
        
        /// <summary>
        /// Actual handling of the DruhStudia request
        /// </summary>
        /// <param name="request">Data related to the katedra</param>
        /// <returns></returns>
        public async Task<DruhStudiaResponse> HandleAsync(DruhStudiaRequest request)
        {
            _logger.LogInformation($"Katedra: '{request.KatedraId}', '{request.SearchText}'");

            var response = new DruhStudiaResponse();

            try
            {
                var katedry = await _dataService.GetDruhStudiaAsync(request.KatedraId, request.SearchText);

                response.Success = true;
                response.DruhyStudia = katedry;

                _logger.LogInformation($"DruhStudia: '{request.KatedraId}', '{request.SearchText}' - found {katedry.Count} results");
            }
            catch (KarolinkaException ex)
            {
                response.Success = true;
                response.DruhyStudia = new List<DruhStudia>();

                _logger.LogError($"DruhStudia: '{request.KatedraId}', '{request.SearchText}' - failed - {ex.Code}");
            }
            catch (Exception)
            {
                var e = new KarolinkaException(KarolinkaException.ExceptionType.UnknownException);
                
                response.Success = false;

                _logger.LogError($"DruhStudia: '{request.KatedraId}', '{request.SearchText}' - failed - {e.Code}");
            }

            return response;
        }
    }
}