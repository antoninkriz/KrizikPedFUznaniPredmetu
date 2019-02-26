using System;

namespace KarolinkaUznani.Common.Requests.Data
{
    /// <summary>
    /// Object containing data related to search queries
    /// </summary>
    public abstract class PredmetRequest : IRequest
    {
        /// <summary>
        /// Text that is the user searching for
        /// </summary>
        public string SearchText { get; set; }
        
        /// <summary>
        /// Id of Obor that we want to get Predmet for
        /// </summary>
        public Guid OborId { get; set; }
    }
}