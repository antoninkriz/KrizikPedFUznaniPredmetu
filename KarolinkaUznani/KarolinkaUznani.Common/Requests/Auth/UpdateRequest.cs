using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KarolinkaUznani.Common.Requests.Auth
{
    /// <inheritdoc />
    /// <summary>
    /// Object containing data related to updating users data 
    /// </summary>
    public class UpdateRequest : IRequest
    {
        /// <summary>
        /// Users GUID
        /// </summary>
        [BindNever]
        public Guid UserId { get; set; }
        
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
    }
}