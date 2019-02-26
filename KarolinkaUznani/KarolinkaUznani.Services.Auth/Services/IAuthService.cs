using System;
using System.Threading.Tasks;
using KarolinkaUznani.Common.Auth.JWT;
using KarolinkaUznani.Common.Responses.Auth;

namespace KarolinkaUznani.Services.Auth.Services
{
    /// <summary>
    /// Interface for the UserService
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Get user info for given user Id
        /// </summary>
        /// <param name="userId">Users guid</param>
        /// <returns></returns>
        Task<User> GetAsync(Guid userId);
        
        /// <summary>
        /// Register an user for given email, password and name
        /// </summary>
        /// <param name="code">Users university code id</param>
        /// <param name="email">Users email</param>
        /// <param name="password">Users password</param>
        /// <param name="name">Users name</param>
        /// <param name="surname">Users surname</param>
        /// <param name="phone">Users phone number</param>
        /// <returns></returns>
        Task<JsonWebToken> RegisterAsync(int code, string email, string password, string name, string surname, string phone);
        
        /// <summary>
        /// Login user for given email and password 
        /// </summary>
        /// <param name="email">Users email</param>
        /// <param name="password">Users password</param>
        /// <returns></returns>
        Task<JsonWebToken> LoginAsync(string email, string password);
    }
}