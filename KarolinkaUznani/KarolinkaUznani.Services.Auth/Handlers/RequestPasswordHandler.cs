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
    public class RequestPasswordHandler : IRequestHandler<PasswordRequest, BasicResponse>
    {
        private readonly IAuthService _authService;
        private readonly ILogger _logger;

        public RequestPasswordHandler(IAuthService authService, ILogger<RequestUpdateHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Actual handling of the user request
        /// </summary>
        /// <param name="request">Data related to the login</param>
        /// <returns></returns>
        public async Task<BasicResponse> HandleAsync(PasswordRequest request)
        {
            _logger.LogInformation($"Password: {request.UserId}'");

            var response = new BasicResponse();

            try
            {
                await _authService.PasswordAsync(request.UserId, request.Password);

                response.Success = true;

                _logger.LogInformation($"Password: '{request.UserId}' - success");
            }
            catch (KarolinkaException ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                _logger.LogError($"Password: '{request.UserId}' - failed - {ex.Code}");
            }
            catch (Exception)
            {
                var e = new KarolinkaException(KarolinkaException.ExceptionType.UnknownException);
                
                response.Success = false;
                response.Message = e.Message;

                _logger.LogError($"Password: '{request.UserId}' - failed - {e.Code}");
            }

            return response;
        }
    }
}