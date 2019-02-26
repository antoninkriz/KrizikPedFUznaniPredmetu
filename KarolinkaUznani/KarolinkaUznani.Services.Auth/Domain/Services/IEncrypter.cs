namespace KarolinkaUznani.Services.Auth.Domain.Services
{
    /// <summary>
    /// Service interface for encryption service
    /// </summary>
    public interface IEncrypter
    {
        /// <summary>
        /// Generates random salt
        /// </summary>
        /// <returns></returns>
        byte[] GetSalt();
        
        /// <summary>
        /// Generates hash for specified input and salt
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <param name="salt">Salt that should be used with the value</param>
        /// <returns>Bytes containing the actual hash</returns>
        byte[] GetHash(string value, byte[] salt);
    }
}