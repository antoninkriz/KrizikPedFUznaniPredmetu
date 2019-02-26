using System;

namespace KarolinkaUznani.Common.Requests.Auth
{
    /// <inheritdoc />
    /// <summary>
    /// Object containing data related to requesting data about a user
    /// </summary>
    public class UserRequest : IRequest
    {
        /// <summary>
        /// Guid of the user to search for
        /// </summary>
        public Guid UserId;
    }
}