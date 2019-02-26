using System.Threading.Tasks;
using KarolinkaUznani.Common.Requests.Data;
using KarolinkaUznani.Common.Responses.Data;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DataController : Controller
    {
        private readonly IBusClient _busClient;
        private readonly ILogger<SampleDataController> _logger;

        public DataController(IBusClient busClient, ILogger<SampleDataController> logger)
        {
            _busClient = busClient;
            _logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Katedra([FromBody] KatedraRequest req)
        {
            var response = await _busClient.RequestAsync<KatedraRequest, KatedraResponse>(req);

            _logger.LogInformation($"Katedra searched - '{req.SearchText}' - {response.Katedry.Count}");

            return Ok(JsonConvert.SerializeObject(response));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DruhStudia([FromBody] DruhStudiaRequest req)
        {
            var response = await _busClient.RequestAsync<DruhStudiaRequest, DruhStudiaResponse>(req);

            _logger.LogInformation($"DruhStudia searched - '{req.SearchText}', '{req.KatedraId}' - {response.DruhyStudia.Count}");

            return Ok(JsonConvert.SerializeObject(response));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Obor([FromBody] OborRequest req)
        {
            var response = await _busClient.RequestAsync<OborRequest, OborResponse>(req);

            _logger.LogInformation($"Obor searched - '{req.SearchText}', '{req.KatedraId}', {req.DruhStudiaId} - {response.Obory.Count}");

            return Ok(JsonConvert.SerializeObject(response));
        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> Predmet([FromBody] PredmetRequest req)
        {
            var response = await _busClient.RequestAsync<PredmetRequest, PredmetResponse>(req);

            _logger.LogInformation($"Obor searched - '{req.SearchText}', '{req.OborId}' - {response.Predmety.Count}");

            return Ok(JsonConvert.SerializeObject(response));
        }
    }
}