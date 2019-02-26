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

        public OborDbModel(Guid id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
    }
}