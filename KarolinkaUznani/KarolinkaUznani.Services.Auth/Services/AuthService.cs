using System;
using System.Threading.Tasks;
using KarolinkaUznani.Common.Auth;
using KarolinkaUznani.Common.Auth.JWT;
using KarolinkaUznani.Common.Exceptions;
using KarolinkaUznani.Common.Responses.Auth;
using KarolinkaUznani.Services.Auth.Domain.Models;
using KarolinkaUznani.Services.Auth.Domain.Repositories;
using KarolinkaUznani.Services.Auth.Domain.Services;

namespace KarolinkaUznani.Services.Auth.Services
{
    /// <inheritdoc />
    /// <summary>
    /// Service related to users
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IEncrypter _encrypter;

        public AuthService(IUserRepository userRepository, IJwtHandler jwtHandler, IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _encrypter = encrypter;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets users info for given user guid
        /// </summary>
        /// <param name="userId">Guid of the user</param>
        /// <returns></returns>
        public async Task<User> GetAsync(Guid userId)
        {
            var userDbModel = await _userRepository.GetAsync(userId);

            if (userDbModel == null)
                throw new KarolinkaException(KarolinkaException.ExceptionType.UserNotExist);
            
            var user = new User(
                userDbModel.Code,
                userDbModel.Email,
                userDbModel.Name,
                userDbModel.Surname,
                userDbModel.Phone
            );

            return user;
        }

        /// <inheritdoc />
        /// <summary>
        /// Register an user for given email, password and name
        /// </summary>
        /// <param name="code">Users university code id</param>
        /// <param name="email">Users email</param>
        /// <param name="passw">Users password</param>
        /// <param name="name">Users name</param>
        /// <param name="surname">Users surname</param>
        /// <param name="phone">Users phone number</param>
        /// <returns></returns>
        public async Task<JsonWebToken> RegisterAsync(int code, string email, string passw, string name, string surname,
            string phone)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new KarolinkaException(KarolinkaException.ExceptionType.EmptyEmail);
            if (string.IsNullOrWhiteSpace(name))
                throw new KarolinkaException(KarolinkaException.ExceptionType.EmptyName);
            if (string.IsNullOrWhiteSpace(surname))
                throw new KarolinkaException(KarolinkaException.ExceptionType.EmptySurname);
            if (string.IsNullOrWhiteSpace(phone))
                throw new KarolinkaException(KarolinkaException.ExceptionType.EmptyPhone);
            if (string.IsNullOrWhiteSpace(passw))
                throw new KarolinkaException(KarolinkaException.ExceptionType.EmptyPassword);

            if (!email.IsValidEmail())
                throw new KarolinkaException(KarolinkaException.ExceptionType.NotEmail);
            if (!passw.IsValidPassword())
                throw new KarolinkaException(KarolinkaException.ExceptionType.NotPassword);

            var user = await _userRepository.GetAsync(email);
            if (user != null)
                throw new KarolinkaException(KarolinkaException.ExceptionType.EmailAlreadyUsed);

            var salt = _encrypter.GetSalt();
            var password = _encrypter.GetHash(passw, salt);

            user = new UserDbModel(
                Guid.NewGuid(),
                code,
                email,
                name,
                surname,
                phone,
                password,
                salt,
                DateTime.UtcNow
            );

            await _userRepository.AddAsync(user);

            return await LoginAsync(email, passw);
        }

        /// <inheritdoc />
        /// <summary>
        /// Login user for given email and password 
        /// </summary>
        /// <param name="email">Users email</param>
        /// <param name="password">Users password</param>
        /// <returns></returns>
        public async Task<JsonWebToken> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);

            if (user == null)
                throw new KarolinkaException(KarolinkaException.ExceptionType.InvalidCredentials);
            if (!user.ValidatePassword(password, _encrypter))
                throw new KarolinkaException(KarolinkaException.ExceptionType.InvalidCredentials);

            return _jwtHandler.Create($"{user.Id}".Replace("-", string.Empty));
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates users info
        /// </summary>
        /// <param name="id">GUID</param>
        /// <param name="code">Users university code id</param>
        /// <param name="email">Users email</param>
        /// <param name="name">Users name</param>
        /// <param name="surname">Users surname</param>
        /// <param name="phone">Users phone number</param>
        /// <returns></returns>
        public async Task UpdateAsync(Guid id, int code, string email, string name, string surname, string phone)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new KarolinkaException(KarolinkaException.ExceptionType.EmptyEmail);
            if (string.IsNullOrWhiteSpace(name))
                throw new KarolinkaException(KarolinkaException.ExceptionType.EmptyName);
            if (string.IsNullOrWhiteSpace(surname))
                throw new KarolinkaException(KarolinkaException.ExceptionType.EmptySurname);
            if (string.IsNullOrWhiteSpace(phone))
                throw new KarolinkaException(KarolinkaException.ExceptionType.EmptyPhone);

            if (!email.IsValidEmail())
                throw new KarolinkaException(KarolinkaException.ExceptionType.NotEmail);
           
            var user = await _userRepository.GetAsync(email);
            if (user != null && !id.Equals(user.Id))
                throw new KarolinkaException(KarolinkaException.ExceptionType.EmailAlreadyUsed);
            
            user = new UserDbModel(
                id,
                code,
                email,
                name,
                surname,
                phone,
                null,
                null,
                DateTime.UnixEpoch
            );

            await _userRepository.UpdateAsync(user);
        }

        /// <inheritdoc />
        /// <summary>
        /// Updates users password
        /// </summary>
        /// <param name="id">GUID</param>
        /// <param name="newPassword">New password</param>
        /// <returns></returns>
        public async Task PasswordAsync(Guid id, string newPassword)
        {
            if (!newPassword.IsValidPassword())
                throw new KarolinkaException(KarolinkaException.ExceptionType.NotPassword);
            
            var salt = _encrypter.GetSalt();
            var password = _encrypter.GetHash(newPassword, salt);

            await _userRepository.PasswordAsync(id, password, salt);
        }

        /// <inheritdoc />
        /// <summary>
        /// Deletes an user
        /// </summary>
        /// <param name="id">GUID</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
                throw new KarolinkaException(KarolinkaException.ExceptionType.UserNotExist);

            await _userRepository.DeleteAsync(id);
        }
    }
}