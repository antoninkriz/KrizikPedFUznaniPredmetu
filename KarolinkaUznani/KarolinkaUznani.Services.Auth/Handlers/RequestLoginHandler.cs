using System.Threading.Tasks;
using KarolinkaUznani.Common.Exceptions;
using KarolinkaUznani.Common.Requests;
using KarolinkaUznani.Common.Requests.Auth;
using KarolinkaUznani.Common.Responses.Auth;
using KarolinkaUznani.Services.Auth.Services;
using Microsoft.Extensions.Logging;

namespace KarolinkaUznani.Services.Auth.Handlers
{
    /// <summary>
    /// Handler for login attempts
    /// </summary>
    public class RequestLoginHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IAuthService _authService;
        private readonly ILogger _logger;

        public RequestLoginHandler(IAuthService authService, ILogger<RequestLoginHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Actual handling of the login
        /// </summary>
        /// <param name="request">Data related to the login</param>
        /// <returns></returns>
        public async Task<LoginResponse> HandleAsync(LoginRequest request)
        {
            _logger.LogInformation($"Login: {request.Email}'");

            var response = new LoginResponse();

            try
            {
                var token = await _authService.LoginAsync(request.Email, request.Password);

                response.Success = true;
                response.Response = token.Token;
                response.Expires = token.Expires;

                _logger.LogInformation($"Login: '{request.Email}' - success");
            }
            catch (KarolinkaException ex)
            {
                response.Success = false;
                response.Response = ex.Code;

                _logger.LogInformation($"Login: '{request.Email}' - failed - {ex.Code}");
            }

            return response;
        }
    }
}