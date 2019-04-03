using System.Threading.Tasks;
using KarolinkaUznani.Common.Services;

namespace KarolinkaUznani.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
            => (await ServiceHost.Create<Startup>(args)
                .UseRabbitMq())
                .Build()
                .Run();
    }
}