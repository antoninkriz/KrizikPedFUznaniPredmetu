namespace KarolinkaUznani.Common.Responses.Auth
{
    /// <inheritdoc />
    /// <summary>
    /// Data related to login response
    /// </summary>
    public class LoginResponse : IResponse
    {
        /// <summary>
        /// Did the login succeed
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// Error message if exists
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// JWT token as a string, if success, else null
        /// </summary>
        public string Token { get; set; }
        
        /// <summary>
        /// Expiration date and time of such token as a linux timestamp
        /// </summary>
        public long Expires { get; set; }
    }
}