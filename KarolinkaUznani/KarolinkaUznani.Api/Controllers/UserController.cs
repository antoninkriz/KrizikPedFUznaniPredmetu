using System;
using System.Threading.Tasks;
using KarolinkaUznani.Common.Requests.Auth;
using KarolinkaUznani.Common.Responses.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RawRabbit;

namespace KarolinkaUznani.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IBusClient _busClient;
        private readonly ILogger<SampleDataController> _logger;

        public UserController(IBusClient busClient, ILogger<SampleDataController> logger)
        {
            _busClient = busClient;
            _logger = logger;
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get()
        {
            var response = await _busClient.RequestAsync<UserRequest, UserResponse>(new UserRequest
            {
                UserId = Guid.Parse(User.Identity.Name)
            });
            var jsonResp = JsonConvert.SerializeObject(response);

            _logger.LogInformation($"User requested - '{User.Identity.Name}' - {(response.Success ? "SUCCESS" : "FAIL")}");
            
            return Ok(jsonResp);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var response = await _busClient.RequestAsync<LoginRequest, LoginResponse>(req);
            var jsonResp = JsonConvert.SerializeObject(response);

            _logger.LogInformation($"Login attempted - '{req.Email}' - {(response.Success ? "SUCCESS" : "FAIL")} - {response.Response}");

            if (response.Success)
                return Ok(jsonResp);
            return Unauthorized(jsonResp);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req)
        {
            var response = await _busClient.RequestAsync<RegisterRequest, LoginResponse>(req);
            var jsonResp = JsonConvert.SerializeObject(response);

            _logger.LogInformation($"Register attempted - '{req.Email}' - {(response.Success ? "SUCCESS" : "FAIL")} - {response.Response}");

            if (response.Success)
                return Ok(jsonResp);
            return Unauthorized(jsonResp);
        }
    }
}