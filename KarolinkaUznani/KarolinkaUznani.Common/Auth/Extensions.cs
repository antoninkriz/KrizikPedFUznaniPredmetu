using System;
using System.Net.Mail;
using System.Text;
using KarolinkaUznani.Common.Auth.JWT;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace KarolinkaUznani.Common.Auth
{
    public static class Extensions
    {
        /// <summary>
        /// Adds JWT to the service
        /// </summary>
        /// <param name="services">Services collection</param>
        /// <param name="configuration">appsettings.json config</param>
        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new JwtOptions();
            var section = configuration.GetSection("jwt");
            section.Bind(options);
            services.Configure<JwtOptions>(configuration.GetSection("jwt"));
            services.AddSingleton<IJwtHandler, JwtHandler>();
            services.AddAuthentication()
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidIssuer = options.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey))
                    };
                });
        }

        /// <summary>
        /// Check if email is valid
        /// </summary>
        /// <param name="address">Email address</param>
        /// <returns></returns>
        public static bool IsValidEmail(this string address)
        {
            try
            {
                new MailAddress(address);
                return true;
            }
            catch (FormatException e)
            {
                return false;
            }
        }

        /// <summary>
        /// Check if password is OK
        /// 6+ characters
        /// Contains lowercase and uppercase character and a number
        /// </summary>
        /// <param name="password">Password to check</param>
        /// <returns></returns>
        public static bool IsValidPassword(this string password)
        {
            if (password.Length < 6)
                return false;

            var hasLowerCase = false;
            var hasUpperCase = false;
            var hasNumber = false;

            foreach (var c in password)
            {
                if (char.IsLower(c)) hasLowerCase = true;
                else if (char.IsUpper(c)) hasUpperCase = true;
                else if (char.IsDigit(c)) hasNumber = true;
            }

            return hasLowerCase && hasUpperCase && hasNumber;
        }
    }
}