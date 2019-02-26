using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KarolinkaUznani.Common.Database;
using KarolinkaUznani.Common.RabbitMq;
using KarolinkaUznani.Common.Requests;
using KarolinkaUznani.Common.Requests.Data;
using KarolinkaUznani.Common.Responses.Data;
using KarolinkaUznani.Services.Data.Domain.Repositories;
using KarolinkaUznani.Services.Data.Domain.Repositories.MySql;
using KarolinkaUznani.Services.Data.Handlers;
using KarolinkaUznani.Services.Data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KarolinkaUznani.Services.Data
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddLogging();
            services.AddRabbitMq(Configuration);
            services.AddDatabase(Configuration, new Dictionary<string, List<(Type, Type)>>
            {
                {
                    "mysql", new List<(Type, Type)>
                    {
                        (typeof(IDataRepository), typeof(MySqlDataRepository))
                    }
                } 
            });
            
            services.AddScoped<IDataService, DataService>();
            services.AddScoped<IRequestHandler<KatedraRequest, KatedraResponse>, RequestKatedraHandler>();
            services.AddScoped<IRequestHandler<DruhStudiaRequest, DruhStudiaResponse>, RequestDruhStudiaHandler>();
            services.AddScoped<IRequestHandler<OborRequest, OborResponse>, RequestOborHandler>();
            services.AddScoped<IRequestHandler<PredmetRequest, PredmetResponse>, RequestPredmetHandler>();
            
            return services.BuildServiceProvider();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ApplicationServices.GetService<IDatabaseInitializer>().InitializeAsync();
            app.UseMvc();
        }
    }
}