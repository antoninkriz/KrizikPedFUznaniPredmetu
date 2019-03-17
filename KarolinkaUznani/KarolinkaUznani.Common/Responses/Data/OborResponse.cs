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

        /// <summary>
        /// Specification of the Obor
        /// </summary>
        public readonly string Specification;

        /// <summary>
        /// Start year of the Obor
        /// </summary>
        public readonly int? YearFrom;
        
        /// <summary>
        /// End year of the Obor
        /// </summary>
        public readonly int? YearTo;

        /// <summary>
        /// Form of studium - True = KS / False = PS
        /// </summary>
        public readonly bool StudyForm;


        public Obor(Guid id, string code, string name, string specification, int? yearFrom, int? yearTo, bool studyForm)
        {
            Id = id;
            Code = code;
            Name = name;
            Specification = specification;
            YearFrom = yearFrom;
            YearTo = yearTo;
            StudyForm = studyForm;
        }
    }
}