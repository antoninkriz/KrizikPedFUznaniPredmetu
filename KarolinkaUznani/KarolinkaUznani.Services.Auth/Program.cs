using System.Threading.Tasks;
using KarolinkaUznani.Common.Requests.Auth;
using KarolinkaUznani.Common.Responses;
using KarolinkaUznani.Common.Responses.Auth;
using KarolinkaUznani.Common.Services;
using Microsoft.EntityFrameworkCore;

namespace KarolinkaUznani.Services.Auth
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceHost = await ServiceHost.Create<Startup>(args)
                .UseRabbitMq();

            await serviceHost.SubscribeToRcp<LoginRequest, LoginResponse>();
            await serviceHost.SubscribeToRcp<RegisterRequest, LoginResponse>();
            await serviceHost.SubscribeToRcp<UserRequest, UserResponse>();
            await serviceHost.SubscribeToRcp<UpdateRequest, BasicResponse>();
            await serviceHost.SubscribeToRcp<PasswordRequest, BasicResponse>();
            await serviceHost.SubscribeToRcp<DeleteRequest, BasicResponse>();

            serviceHost.Build().Run();
        }
    }
}