using System;
using System.Threading.Tasks;
using KarolinkaUznani.Services.Auth.Domain.Models;

namespace KarolinkaUznani.Services.Auth.Domain.Repositories
{
    /// <summary>
    /// Interface for the UserRepository
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get an User for specified GUID
        /// </summary>
        /// <param name="id">GUID</param>
        /// <returns>User or null</returns>
        Task<UserDbModel> GetAsync(Guid id);
        
        /// <summary>
        /// Get an User for specified email
        /// </summary>
        /// <param name="email">Email of the user</param>
        /// <returns>User or null</returns>
        Task<UserDbModel> GetAsync(string email);
        
        /// <summary>
        /// Add an User in to the database 
        /// </summary>
        /// <param name="user">User model</param>
        /// <returns></returns>
        Task AddAsync(UserDbModel user);
        
        /// <summary>
        /// Updates an User in the database 
        /// </summary>
        /// <param name="user">User model, without password, salt, createdAt</param>
        /// <returns></returns>
        Task UpdateAsync(UserDbModel user);

        /// <summary>
        /// Updates users password in the database
        /// </summary>
        /// <param name="id">GUID</param>
        /// <param name="password">Users hashed and salted password</param>
        /// <param name="salt">Salt for the password</param>
        /// <returns></returns>
        Task PasswordAsync(Guid id, byte[] password, byte[] salt);

        /// <summary>
        /// Deletes an User from the database
        /// </summary>
        /// <param name="id">GUID</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
    }
}