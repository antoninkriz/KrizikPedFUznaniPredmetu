using System;
using System.Collections.Generic;
using KarolinkaUznani.Common.Auth;
using KarolinkaUznani.Common.Database;
using KarolinkaUznani.Common.RabbitMq;
using KarolinkaUznani.Common.Requests;
using KarolinkaUznani.Common.Requests.Auth;
using KarolinkaUznani.Common.Responses.Auth;
using KarolinkaUznani.Services.Auth.Domain.Repositories;
using KarolinkaUznani.Services.Auth.Domain.Repositories.MySql;
using KarolinkaUznani.Services.Auth.Domain.Services;
using KarolinkaUznani.Services.Auth.Handlers;
using KarolinkaUznani.Services.Auth.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KarolinkaUznani.Services.Auth
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
            services.AddJwt(Configuration);
            services.AddRabbitMq(Configuration);
            services.AddDatabase(Configuration, new Dictionary<string, List<(Type, Type)>>
            {
                {
                    "mysql", new List<(Type, Type)>
                    {
                        (typeof(IUserRepository), typeof(MySqlUserRepository))
                    }
                }
            });
            
            services.AddScoped<IEncrypter, Encrypter>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRequestHandler<LoginRequest, LoginResponse>, RequestLoginHandler>();
            services.AddScoped<IRequestHandler<RegisterRequest, LoginResponse>, RequestRegisterHandler>();
            services.AddScoped<IRequestHandler<UserRequest, UserResponse>, RequestUserHandler>();

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