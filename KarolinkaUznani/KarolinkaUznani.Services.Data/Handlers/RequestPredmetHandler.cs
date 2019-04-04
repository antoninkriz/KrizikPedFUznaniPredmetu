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
    /// Handler for Predmet request
    /// </summary>
    public class RequestPredmetHandler : IRequestHandler<PredmetRequest, PredmetResponse>
    {
        private readonly IDataService _dataService;
        private readonly ILogger _logger;

        public RequestPredmetHandler(IDataService dataService, ILogger<RequestPredmetHandler> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }
        
        /// <summary>
        /// Actual handling of the Predmet request
        /// </summary>
        /// <param name="request">Data related to the predmet</param>
        /// <returns></returns>
        public async Task<PredmetResponse> HandleAsync(PredmetRequest request)
        {
            _logger.LogInformation($"Katedra: '{request.OborId}', '{request.SearchText}'");

            var response = new PredmetResponse();

            try
            {
                var predmety = await _dataService.GetPredmetAsync(request.OborId, request.SearchText);

                response.Success = true;
                response.Predmety = predmety;

                _logger.LogInformation($"Predmet: '{request.OborId}', '{request.SearchText}' - found {predmety.Count} results");
            }
            catch (KarolinkaException ex)
            {
                response.Success = true;
                response.Predmety = new List<Predmet>();

                _logger.LogError($"Predmet: '{request.OborId}', '{request.SearchText}' - failed - {ex.Code}");
            }
            catch (Exception)
            {
                var e = new KarolinkaException(KarolinkaException.ExceptionType.UnknownException);
                
                response.Success = false;

                _logger.LogError($"Predmet: '{request.OborId}', '{request.SearchText}' - failed - {e.Code}");
                
            }

            return response;
        }
    }
}