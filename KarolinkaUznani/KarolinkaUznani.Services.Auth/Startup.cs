using System;
using System.Collections.Generic;
using KarolinkaUznani.Common.Auth;
using KarolinkaUznani.Common.Database;
using KarolinkaUznani.Common.RabbitMq;
using KarolinkaUznani.Common.Requests;
using KarolinkaUznani.Common.Requests.Auth;
using KarolinkaUznani.Common.Responses;
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
            services.AddScoped<IRequestHandler<UpdateRequest, BasicResponse>, RequestUpdateHandler>();
            services.AddScoped<IRequestHandler<PasswordRequest, BasicResponse>, RequestPasswordHandler>();
            services.AddScoped<IRequestHandler<DeleteRequest, BasicResponse>, RequestDeleteHandler>();

            return services.BuildServiceProvider();
        }

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