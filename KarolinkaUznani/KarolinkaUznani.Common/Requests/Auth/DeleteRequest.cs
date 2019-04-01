using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KarolinkaUznani.Common.Requests.Auth
{
    /// <inheritdoc />
    /// <summary>
    /// Object containing data related to deleting an user
    /// </summary>
    public class DeleteRequest : IRequest
    {
        /// <summary>
        /// Guid of the user to search for
        /// </summary>
        [BindNever]
        public Guid UserId { get; set; }
    }
}