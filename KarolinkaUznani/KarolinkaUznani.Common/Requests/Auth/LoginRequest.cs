namespace KarolinkaUznani.Common.Requests.Auth
{
    /// <summary>
    /// Object containing data related to user login
    /// </summary>
    public class LoginRequest : IRequest
    {
        /// <summary>
        /// Email to try to login with
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Password that should be compared with hash related to the email
        /// </summary>
        public string Password { get; set; }
    }
}