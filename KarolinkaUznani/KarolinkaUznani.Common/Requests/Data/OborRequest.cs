using System;

namespace KarolinkaUznani.Common.Requests.Data
{
    /// <summary>
    /// Object containing data related to search queries
    /// </summary>
    public class OborRequest : IRequest
    {
        /// <summary>
        /// Text that is the user searching for
        /// </summary>
        public string SearchText { get; set; }
        
        /// <summary>
        /// Id of Katedra that we want to get Obor for
        /// </summary>
        public Guid KatedraId { get; set; }
        
        /// <summary>
        /// Id of DruhStudia that we want to get Obor for
        /// </summary>
        public Guid DruhStudiaId { get; set; }
    }
}