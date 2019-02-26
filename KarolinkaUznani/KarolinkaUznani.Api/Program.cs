using KarolinkaUznani.Common.Services;

namespace KarolinkaUznani.Api
{
    public class Program
    {
        public static void Main(string[] args)
            => ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .Build()
                .Run();
    }
}