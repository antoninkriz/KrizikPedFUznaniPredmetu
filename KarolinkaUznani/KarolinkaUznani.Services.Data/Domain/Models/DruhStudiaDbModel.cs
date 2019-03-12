using System;

namespace KarolinkaUznani.Services.Data.Domain.Models
{
    /// <summary>
    /// DruhStudia database model
    /// </summary>
    public class DruhStudiaDbModel
    {
        /// <summary>
        /// GUID of the DruhStudia
        /// </summary>
        public readonly Guid Id;

        /// <summary>
        /// Code of the DruhStudia
        /// </summary>
        public readonly string Code;

        /// <summary>
        /// Name of the DruhStudia
        /// </summary>
        public readonly string Name;

        public DruhStudiaDbModel(Guid id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
    }
}