using System;
using System.Threading.Tasks;
using KarolinkaUznani.Common.Exceptions;
using KarolinkaUznani.Common.Requests;
using KarolinkaUznani.Common.Requests.Auth;
using KarolinkaUznani.Common.Responses;
using KarolinkaUznani.Services.Auth.Services;
using Microsoft.Extensions.Logging;

namespace KarolinkaUznani.Services.Auth.Handlers
{
    /// <summary>
    /// Handler for login attempts
    /// </summary>
    public class RequestUpdateHandler : IRequestHandler<UpdateRequest, BasicResponse>
    {
        private readonly IAuthService _authService;
        private readonly ILogger _logger;

        public RequestUpdateHandler(IAuthService authService, ILogger<RequestUpdateHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Actual handling of the user request
        /// </summary>
        /// <param name="request">Data related to the login</param>
        /// <returns></returns>
        public async Task<BasicResponse> HandleAsync(UpdateRequest request)
        {
            _logger.LogInformation($"Update: {request.UserId}'");

            var response = new BasicResponse();

            try
            {
                await _authService.UpdateAsync(request.UserId, request.Code, request.Email, request.Name, request.Surname, request.Phone);

                response.Success = true;

                _logger.LogInformation($"Update: '{request.UserId}' - success");
            }
            catch (KarolinkaException ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                _logger.LogError($"Update: '{request.UserId}' - failed - {ex.Code}");
            }
            catch (Exception)
            {
                var e = new KarolinkaException(KarolinkaException.ExceptionType.UnknownException);
                
                response.Success = false;
                response.Message = e.Message;

                _logger.LogError($"Update: '{request.UserId}' - failed - {e.Code}");
            }

            return response;
        }
    }
}