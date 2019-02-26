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
    /// Handler for registration attempts
    /// </summary>
    public class RequestRegisterHandler : IRequestHandler<RegisterRequest, LoginResponse>
    {
        private readonly IAuthService _authService;
        private readonly ILogger _logger;

        public RequestRegisterHandler(IAuthService authService, ILogger<RequestLoginHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Actual handling of the registration
        /// </summary>
        /// <param name="request">Data related to the registration</param>
        /// <returns>LoginResponse - we will automatically login the user</returns>
        public async Task<LoginResponse> HandleAsync(RegisterRequest request)
        {
            _logger.LogInformation($"Registration: '{request.Email}'");

            var response = new LoginResponse();

            try
            {
                var token = await _authService.RegisterAsync(request.Code, request.Email, request.Password, request.Name, request.Surname, request.Phone);

                response.Success = true;
                response.Response = token.Token;
                response.Expires = token.Expires;

                _logger.LogInformation($"Registration: '{request.Email}' - success");
            }
            catch (KarolinkaException ex)
            {
                response.Success = false;
                response.Response = ex.Code;

                _logger.LogInformation($"Registration: '{request.Email}' - failed - {ex.Code}");
            }

            return response;
        }
    }
}