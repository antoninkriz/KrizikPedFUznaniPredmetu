using System;

namespace KarolinkaUznani.Services.Data.Domain.Models
{
    /// <summary>
    /// Predmet database model
    /// </summary>
    public class PredmetDbModel
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

        public PredmetDbModel(Guid id, string code, string name, string zkouska)
        {
            Id = id;
            Code = code;
            Name = name;
            Zkouska = zkouska;
        }
    }
}