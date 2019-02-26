using System.Threading.Tasks;
using KarolinkaUznani.Common.Requests.Auth;
using KarolinkaUznani.Common.Responses.Auth;
using KarolinkaUznani.Common.Services;

namespace KarolinkaUznani.Services.Auth
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceHost = ServiceHost.Create<Startup>(args)
                .UseRabbitMq();

            await serviceHost.SubscribeToRcp<LoginRequest, LoginResponse>();
            await serviceHost.SubscribeToRcp<RegisterRequest, LoginResponse>();

            serviceHost.Build().Run();
        }
    }
}