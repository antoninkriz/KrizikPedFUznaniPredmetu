namespace KarolinkaUznani.Common.Responses.Auth
{
    /// <inheritdoc />
    /// <summary>
    /// Data related to user response
    /// </summary>
    public class UserResponse : IResponse
    {
        /// <summary>
        /// Was the user found successfuly?
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Users name
        /// </summary>
        public User User { get; set; }
    }

    /// <summary>
    /// Object defining Katedra and its data
    /// </summary>
    public class User
    {
        /// <summary>
        /// Users university code id
        /// </summary>
        public int Code { get; set; }
        
        /// <summary>
        /// Users email
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Users name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Users surname
        /// </summary>
        public string Surname { get; set; }
        
        /// <summary>
        /// Users phone number
        /// </summary>
        public string Phone { get; set; }

        public User(int code, string email, string name, string surname, string phone)
        {
            Code = code;
            Email = email;
            Name = name;
            Surname = surname;
            Phone = phone;
        }
    }
}