using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KarolinkaUznani.Common.Database;
using KarolinkaUznani.Services.Auth.Domain.Models;
using MySql.Data.MySqlClient;

namespace KarolinkaUznani.Services.Auth.Domain.Repositories.MySql
{
    /// <inheritdoc cref="IUserRepository" />
    /// <summary>
    /// Handling database stuff related to the user
    /// </summary>
    public class MySqlUserRepository : BaseMySqlRepository, IUserRepository
    {
        public MySqlUserRepository(MySqlConnection dbConnection) : base(dbConnection)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Get an User for specified GUID
        /// </summary>
        /// <param name="id">GUID</param>
        /// <returns>User or null</returns>
        public async Task<UserDbModel> GetAsync(Guid id)
        {
            using (var command = Command("sp_UserByGuid", new List<Param>
            {
                new Param("p_userId", MySqlDbType.Binary, 16, id.ToByteArray())
            }))
            {
                await OpenConnectionAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var i = 0;
                        
                        return new UserDbModel(
                            id: new Guid(await reader.GetFieldValueAsync<byte[]>(i++)),
                            code: await reader.GetFieldValueAsync<int>(i++),
                            email: await reader.GetFieldValueAsync<string>(i++),
                            name: await reader.GetFieldValueAsync<string>(i++),
                            surname: await reader.GetFieldValueAsync<string>(i++),
                            phone: await reader.GetFieldValueAsync<string>(i++),
                            password: await reader.GetFieldValueAsync<byte[]>(i++),
                            salt: await reader.GetFieldValueAsync<byte[]>(i++),
                            createdAt: await reader.GetFieldValueAsync<DateTime>(i)
                        );
                    }
                }
            }

            return null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Get an User for specified email
        /// </summary>
        /// <param name="email">Email of the user</param>
        /// <returns>User or null</returns>
        public async Task<UserDbModel> GetAsync(string email)
        {
            using (var command = Command("sp_UserByEmail", new List<Param>
            {
                new Param("p_userEmail", MySqlDbType.VarChar, 255, email)
            }))
            {
                await OpenConnectionAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var i = 0;
                        
                        return new UserDbModel(
                            id: new Guid(await reader.GetFieldValueAsync<byte[]>(i++)),
                            code: await reader.GetFieldValueAsync<int>(i++),
                            email: await reader.GetFieldValueAsync<string>(i++),
                            name: await reader.GetFieldValueAsync<string>(i++),
                            surname: await reader.GetFieldValueAsync<string>(i++),
                            phone: await reader.GetFieldValueAsync<string>(i++),
                            password: await reader.GetFieldValueAsync<byte[]>(i++),
                            salt: await reader.GetFieldValueAsync<byte[]>(i++),
                            createdAt: await reader.GetFieldValueAsync<DateTime>(i)
                        );
                    }
                }
            }

            return null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Add an User in to the database 
        /// </summary>
        /// <param name="user">User model</param>
        /// <returns></returns>
        public async Task AddAsync(UserDbModel user)
        {
            using (var command = Command("sp_UserAdd", new List<Param>
            {
                new Param("p_userId", MySqlDbType.Binary, 16, user.Id.ToByteArray()),
                new Param("p_userCode", MySqlDbType.Int32, null, user.Code),
                new Param("p_userEmail", MySqlDbType.VarChar, 255, user.Email),
                new Param("p_userName", MySqlDbType.VarChar, 255, user.Name),
                new Param("p_userSurname", MySqlDbType.VarChar, 255, user.Surname),
                new Param("p_userPhone", MySqlDbType.VarChar, 16, user.Phone),
                new Param("p_userPassword", MySqlDbType.Binary, 64, user.Password),
                new Param("p_userSalt", MySqlDbType.Binary, 40, user.Salt),
                new Param("p_userCreatedAt", MySqlDbType.DateTime, null, user.CreatedAt)
            }))
            {
                await OpenConnectionAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}