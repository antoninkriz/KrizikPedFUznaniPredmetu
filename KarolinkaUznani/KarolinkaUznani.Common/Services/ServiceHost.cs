using System;
using System.Threading.Tasks;
using KarolinkaUznani.Common.Commands;
using KarolinkaUznani.Common.Events;
using KarolinkaUznani.Common.RabbitMq;
using KarolinkaUznani.Common.Requests;
using KarolinkaUznani.Common.Responses;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client.Exceptions;
using RawRabbit;

namespace KarolinkaUznani.Common.Services
{
    public class ServiceHost : IServiceHost
    {
        private readonly IWebHost _webHost;

        public ServiceHost(IWebHost webHost)
        {
            _webHost = webHost;
        }

        public void Run()
        {
            _webHost.Run();
        }

        public static HostBuilder Create<TStartup>(string[] args) where TStartup : class
        {
            Console.Title = typeof(TStartup).Namespace;

            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var webHostBuilder = WebHost.CreateDefaultBuilder()
                .UseConfiguration(config)
                .UseStartup<TStartup>();

            return new HostBuilder(webHostBuilder.Build());
        }

        public abstract class BuilderBase
        {
            public abstract ServiceHost Build();
        }

        public class HostBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;

            private IBusClient _bus;

            public HostBuilder(IWebHost webHost)
            {
                _webHost = webHost;
            }

            public async Task<BusBuilder> UseRabbitMq()
            {
                const int retries = 10;
                const int timeout = 5000;
                for (var i = 0; i < retries; i ++) {
                    try
                    {
                        _bus = (IBusClient) _webHost.Services.GetService(typeof(IBusClient));
                        break;
                    }
                    catch (BrokerUnreachableException)
                    {
                        Console.WriteLine($"Waiting for RabbitMq server - {i + 1}/{retries} - waiting for {timeout}ms");
                        await Task.Delay(timeout);
                    }
                }
            
                return new BusBuilder(_webHost, _bus);
            }

            public override ServiceHost Build()
                => new ServiceHost(_webHost);
        }

        public class BusBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;

            private readonly IBusClient _bus;

            public BusBuilder(IWebHost webHost, IBusClient bus)
            {
                _webHost = webHost;
                _bus = bus;
            }

            public async Task<BusBuilder> SubscribeToCommand<TCommand>() where TCommand : ICommand
            {
                var handler = (ICommandHandler<TCommand>) _webHost.Services
                    .GetService(typeof(ICommandHandler<TCommand>));
                await _bus.WithCommandHandlerAsync(handler);

                return this;
            }

            public async Task<BusBuilder> SubscribeToEvent<TEvent>() where TEvent : IEvent
            {
                var handler = (IEventHandler<TEvent>) _webHost.Services
                    .GetService(typeof(IEventHandler<TEvent>));
                await _bus.WithEventHandlerAsync(handler);

                return this;
            }

            public async Task<BusBuilder> SubscribeToRcp<TRequest, TResponse>()
                where TRequest : IRequest where TResponse : IResponse
            {
                var handler = (IRequestHandler<TRequest, TResponse>) _webHost.Services
                    .GetService(typeof(IRequestHandler<TRequest, TResponse>));
                await _bus.WithRequestResponseAsync(handler);

                return this;
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }
    }
}