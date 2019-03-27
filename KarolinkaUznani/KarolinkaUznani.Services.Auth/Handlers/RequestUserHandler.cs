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
    public class RequestUserHandler : IRequestHandler<UserRequest, UserResponse>
    {
        private readonly IAuthService _authService;
        private readonly ILogger _logger;

        public RequestUserHandler(IAuthService authService, ILogger<RequestLoginHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Actual handling of the user request
        /// </summary>
        /// <param name="request">Data related to the login</param>
        /// <returns></returns>
        public async Task<UserResponse> HandleAsync(UserRequest request)
        {
            _logger.LogInformation($"User: {request.UserId}'");

            var response = new UserResponse();

            try
            {
                var user = await _authService.GetAsync(request.UserId);

                response.Success = true;
                response.User = user;

                _logger.LogInformation($"User: '{request.UserId}' - success");
            }
            catch (KarolinkaException ex)
            {
                response.Success = false;
                response.Message = ex.Code;

                _logger.LogInformation($"User: '{request.UserId}' - failed - {ex.Code}");
            }

            return response;
        }
    }
}