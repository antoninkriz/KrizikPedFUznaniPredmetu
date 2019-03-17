using System;

namespace KarolinkaUznani.Services.Data.Domain.Models
{
    /// <summary>
    /// Obor database model
    /// </summary>
    public class OborDbModel
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

        public OborDbModel(Guid id, string code, string name, string specification, int? yearFrom, int? yearTo, bool studyForm)
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