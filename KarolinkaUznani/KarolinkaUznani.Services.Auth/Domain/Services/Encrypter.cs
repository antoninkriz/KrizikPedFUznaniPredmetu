using System.Security.Cryptography;

namespace KarolinkaUznani.Services.Auth.Domain.Services
{
    /// <summary>
    /// Encryption service to generate hashes and salts
    /// </summary>
    public class Encrypter : IEncrypter
    {
        /// <summary>
        /// Size of the salt in bytes
        /// </summary>
        private const int SaltSize = 40;
        
        /// <summary>
        /// Size of the password in bytes
        /// </summary>
        private const int PasswrodSize = 64;
        
        /// <summary>
        /// How strong should the encryption be?
        /// </summary>
        private const int DeriveBytesIterationsCount = 10000;

        /// <inheritdoc />
        /// <summary>
        /// Generates random salt
        /// </summary>
        /// <returns></returns>
        public byte[] GetSalt()
        {
            var saltBytes = new byte[SaltSize];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            return saltBytes;
        }

        /// <inheritdoc />
        /// <summary>
        /// Generates hash for specified input and salt
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <param name="salt">Salt that should be used with the value</param>
        /// <returns>Bytes containing the actual hash</returns>
        public byte[] GetHash(string value, byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(value, salt, DeriveBytesIterationsCount);

            return pbkdf2.GetBytes(PasswrodSize);
        }
    }
}