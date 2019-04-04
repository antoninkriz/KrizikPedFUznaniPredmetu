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
    /// Handler for katedra request
    /// </summary>
    public class RequestKatedraHandler : IRequestHandler<KatedraRequest, KatedraResponse>
    {
        private readonly IDataService _dataService;
        private readonly ILogger _logger;

        public RequestKatedraHandler(IDataService dataService, ILogger<RequestKatedraHandler> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }
        
        /// <summary>
        /// Actual handling of the Katedra request
        /// </summary>
        /// <param name="request">Data related to the katedra</param>
        /// <returns></returns>
        public async Task<KatedraResponse> HandleAsync(KatedraRequest request)
        {
            _logger.LogInformation($"Katedra: '{request.SearchText}'");

            var response = new KatedraResponse();

            try
            {
                var katedry = await _dataService.GetKatedraAsync(request.SearchText);

                response.Success = true;
                response.Katedry = katedry;

                _logger.LogInformation($"Katedra: '{request.SearchText}' - found {katedry.Count} results");
            }
            catch (KarolinkaException ex)
            {
                response.Success = true;
                response.Katedry = new List<Katedra>();

                _logger.LogError($"Katedra: '{request.SearchText}' - failed - {ex.Code}");
            }
            catch (Exception)
            {
                var e = new KarolinkaException(KarolinkaException.ExceptionType.UnknownException);
                
                response.Success = false;

                _logger.LogError($"DruhStudia: '{request.SearchText}' - failed - {e.Code}");
            }

            return response;
        }
    }
}