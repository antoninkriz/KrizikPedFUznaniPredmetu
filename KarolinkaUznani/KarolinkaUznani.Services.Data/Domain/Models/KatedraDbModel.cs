using System;

namespace KarolinkaUznani.Services.Data.Domain.Models
{
    /// <summary>
    /// Katedra database model
    /// </summary>
    public class KatedraDbModel
    {
        /// <summary>
        /// GUID of the Katedra
        /// </summary>
        public readonly Guid Id;

        /// <summary>
        /// Name of the Katedra
        /// </summary>
        public readonly string Name;

        public KatedraDbModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}