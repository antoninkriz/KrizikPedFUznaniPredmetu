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
    public class RequestOborHandler : IRequestHandler<OborRequest, OborResponse>
    {
        private readonly IDataService _dataService;
        private readonly ILogger _logger;

        public RequestOborHandler(IDataService dataService, ILogger<RequestOborHandler> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }
        
        /// <summary>
        /// Actual handling of the Obor request
        /// </summary>
        /// <param name="request">Data related to the obor</param>
        /// <returns></returns>
        public async Task<OborResponse> HandleAsync(OborRequest request)
        {
            _logger.LogInformation($"Obor: '{request.KatedraId}', '{request.DruhStudiaId}', '{request.SearchText}'");

            var response = new OborResponse();

            try
            {
                var obory = await _dataService.GetOborAsync(request.KatedraId, request.DruhStudiaId, request.SearchText);

                response.Success = true;
                response.Obory = obory;

                _logger.LogInformation($"Obor: '{request.KatedraId}', '{request.DruhStudiaId}', '{request.SearchText}' - found {obory.Count} results");
            }
            catch (KarolinkaException ex)
            {
                response.Success = true;
                response.Obory = new List<Obor>();

                _logger.LogError($"Obor: '{request.KatedraId}', '{request.DruhStudiaId}', '{request.SearchText}' - failed - {ex.Code}");
            }
            catch (Exception)
            {
                var e = new KarolinkaException(KarolinkaException.ExceptionType.UnknownException);
                
                response.Success = false;

                _logger.LogError($"Obor: '{request.KatedraId}', '{request.DruhStudiaId}', '{request.SearchText}' - failed - {e.Code}");
            }

            return response;
        }
    }
}