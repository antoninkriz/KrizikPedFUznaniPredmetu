namespace KarolinkaUznani.Common.Requests.Auth
{
    /// <summary>
    /// Object containing data related to registering a new user
    /// </summary>
    public class RegisterRequest : IRequest
    {
        /// <summary>
        /// Users university code id
        /// </summary>
        public int Code { get; set; }
        
        /// <summary>
        /// Email to register the new user with
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// New users name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// New users surname
        /// </summary>
        public string Surname { get; set; }
        
        /// <summary>
        /// New users phone
        /// </summary>
        public string Phone { get; set; }
    
        /// <summary>
        /// Password that will be hashed and saved
        /// </summary>
        public string Password { get; set; }
    }
}