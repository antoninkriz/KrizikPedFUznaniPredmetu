using System;
using System.Collections.Generic;

namespace KarolinkaUznani.Common.Responses.Data
{
    /// <summary>
    /// Data related to the response for Katedra request
    /// </summary>
    public class KatedraResponse : IResponse
    {
        /// <summary>
        /// Was the request successful?
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// List of Katedras that match the requests parameters
        /// </summary>
        public List<Katedra> Katedry { get; set; } 
    }

    /// <summary>
    /// Object defining Katedra and its data
    /// </summary>
    public class Katedra
    {
        /// <summary>
        /// GUID of the Katedra
        /// </summary>
        public readonly Guid Id;

        /// <summary>
        /// Name of the Katedra
        /// </summary>
        public readonly string Name;

        public Katedra(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}