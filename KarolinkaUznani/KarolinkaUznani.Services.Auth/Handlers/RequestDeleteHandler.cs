using System.Threading.Tasks;
using KarolinkaUznani.Common.Exceptions;
using KarolinkaUznani.Common.Requests;
using KarolinkaUznani.Common.Requests.Auth;
using KarolinkaUznani.Common.Responses;
using KarolinkaUznani.Common.Responses.Auth;
using KarolinkaUznani.Services.Auth.Services;
using Microsoft.Extensions.Logging;

namespace KarolinkaUznani.Services.Auth.Handlers
{
    /// <summary>
    /// Handler for login attempts
    /// </summary>
    public class RequestDeleteHandler : IRequestHandler<DeleteRequest, BasicResponse>
    {
        private readonly IAuthService _authService;
        private readonly ILogger _logger;

        public RequestDeleteHandler(IAuthService authService, ILogger<RequestLoginHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Actual handling of the user request
        /// </summary>
        /// <param name="request">Data related to the login</param>
        /// <returns></returns>
        public async Task<BasicResponse> HandleAsync(DeleteRequest request)
        {
            _logger.LogInformation($"Delete: {request.UserId}'");

            var response = new BasicResponse();

            try
            {
                await _authService.GetAsync(request.UserId);

                response.Success = true;

                _logger.LogInformation($"Delete: '{request.UserId}' - success");
            }
            catch (KarolinkaException ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                _logger.LogInformation($"Delete: '{request.UserId}' - failed - {ex.Code}");
            }

            return response;
        }
    }
}