using System;
using System.Collections.Generic;

namespace KarolinkaUznani.Common.Responses.Data
{
    /// <summary>
    /// Data related to the response for Obor request
    /// </summary>
    public class OborResponse : IResponse
    {
        /// <summary>
        /// Was the request successful?
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// List of DruhStudia that match the requests parameters
        /// </summary>
        public List<Obor> Obory { get; set; } 
    }

    /// <summary>
    /// Object defining DruhStudia and its data
    /// </summary>
    public class Obor
    {
        /// <summary>
        /// GUID of the Obor
        /// </summary>
        public readonly Guid Id;

        /// <summary>
        /// Code of the Obor
        /// </summary>
        public readonly string Code;

        /// <summary>
        /// Name of the Obor
        /// </summary>
        public readonly string Name;

        public Obor(Guid id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
    }
}