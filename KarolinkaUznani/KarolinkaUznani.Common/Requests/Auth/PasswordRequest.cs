using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KarolinkaUznani.Common.Requests.Auth
{
    /// <inheritdoc />
    /// <summary>
    /// Object containing data related to changing users password 
    /// </summary>
    /// 
    public class PasswordRequest : IRequest
    {
        /// <summary>
        /// Users GUID
        /// </summary>
        [BindNever]
        public Guid UserId { get; set; }
        
        /// <summary>
        /// New password
        /// </summary>
        public string Password { get; set; }
    }
}