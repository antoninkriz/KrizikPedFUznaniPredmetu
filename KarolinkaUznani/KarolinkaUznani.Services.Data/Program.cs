using System.Threading.Tasks;
using KarolinkaUznani.Common.Requests.Data;
using KarolinkaUznani.Common.Responses.Data;
using KarolinkaUznani.Common.Services;

namespace KarolinkaUznani.Services.Data
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceHost = await ServiceHost.Create<Startup>(args)
                .UseRabbitMq();

            await serviceHost.SubscribeToRcp<KatedraRequest, KatedraResponse>();
            await serviceHost.SubscribeToRcp<DruhStudiaRequest, DruhStudiaResponse>();
            await serviceHost.SubscribeToRcp<OborRequest, OborResponse>();
            await serviceHost.SubscribeToRcp<PredmetRequest, PredmetResponse>();

            serviceHost.Build().Run();
        }
    }
}