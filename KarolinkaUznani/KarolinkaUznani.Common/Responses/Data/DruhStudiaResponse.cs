using System;
using System.Collections.Generic;

namespace KarolinkaUznani.Common.Responses.Data
{
    /// <summary>
    /// Data related to the response for DruhStudia request
    /// </summary>
    public class DruhStudiaResponse : IResponse
    {
        /// <summary>
        /// Was the request successful?
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// List of DruhStudia that match the requests parameters
        /// </summary>
        public List<DruhStudia> DruhyStudia { get; set; } 
    }

    /// <summary>
    /// Object defining DruhStudia and its data
    /// </summary>
    public class DruhStudia
    {
        /// <summary>
        /// GUID of the DruhStudia
        /// </summary>
        public readonly Guid Id;

        /// <summary>
        /// Name of the DruhStudia
        /// </summary>
        public readonly string Name;

        public DruhStudia(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}