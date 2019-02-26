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
        /// Name of the DruhStudia
        /// </summary>
        public readonly string Name;

        public DruhStudiaDbModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}