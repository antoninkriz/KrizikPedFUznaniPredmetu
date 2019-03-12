using System;
using System.Collections.Generic;

namespace KarolinkaUznani.Common.Responses.Data
{
    /// <summary>
    /// Data related to the response for Predmet request
    /// </summary>
    public class PredmetResponse : IResponse
    {
        /// <summary>
        /// Was the request successful?
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// List of Predmet that match the requests parameters
        /// </summary>
        public List<Predmet> Predmety { get; set; } 
    }

    /// <summary>
    /// Object defining Predmet and its data
    /// </summary>
    public class Predmet
    {
        /// <summary>
        /// GUID of the Predmet
        /// </summary>
        public readonly Guid Id;

        /// <summary>
        /// Code of the Predmet
        /// </summary>
        public readonly string Code;

        /// <summary>
        /// Name of the Predmet
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Zkouska of the Predmet
        /// </summary>
        public readonly string Zkouska;

        /// <summary>
        /// Amounts of Kredity student can get for a Predmet
        /// </summary>
        public readonly int Kredity;

        public Predmet(Guid id, string code, string name, string zkouska, int kredity)
        {
            Id = id;
            Code = code;
            Name = name;
            Zkouska = zkouska;
            Kredity = kredity;
        }
    }
}