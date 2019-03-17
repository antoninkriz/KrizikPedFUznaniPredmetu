using System;
using System.Linq;
using KarolinkaUznani.Common.Exceptions;
using KarolinkaUznani.Services.Auth.Domain.Services;

namespace KarolinkaUznani.Services.Auth.Domain.Models
{
    public class UserDbModel
    {
        /// <summary>
        /// Unique GUID of an user
        /// </summary>
        public readonly Guid Id;
        
        /// <summary>
        /// Unique users university code id
        /// </summary>
        public readonly int Code;

        /// <summary>
        /// Unique email of an user
        /// </summary>
        public readonly string Email;

        /// <summary>
        /// Users name
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Users surname
        /// </summary>
        public readonly string Surname;

        /// <summary>
        /// USers phone
        /// </summary>
        public readonly string Phone;

        /// <summary>
        /// Users hashed and salted password
        /// </summary>
        public readonly byte[] Password;

        /// <summary>
        /// Salt for the password 
        /// </summary>
        public readonly byte[] Salt;

        /// <summary>
        /// Date and time fo creation of such user
        /// </summary>
        public readonly DateTime CreatedAt;
        
        public UserDbModel(Guid id, int code, string email, string name, string surname, string phone, byte[] password, byte[] salt, DateTime createdAt)
        {
            Id = id;
            Code = code;
            Email = email;
            Name = name;
            Surname = surname;
            Phone = phone;
            Password = password;
            Salt = salt;
            CreatedAt = createdAt;
        }

        public bool ValidatePassword(string password, IEncrypter encrypter)
            => Password.SequenceEqual(encrypter.GetHash(password, Salt));
    }
}