using System;
using System.Collections.Generic;

namespace KarolinkaUznani.Common.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// Custom extended exception 
    /// </summary>
    public class KarolinkaException : Exception
    {
        /// <summary>
        /// Code of the exception
        /// </summary>
        public string Code { get; }
        
        /// <inheritdoc />
        /// <summary>
        /// Message that might be shown to the user 
        /// </summary>
        public override string Message { get; }

        /// <summary>
        /// Enum of all exceptions that could occur
        /// </summary>
        public enum ExceptionType
        {
            #region Base 100

            UnknownException = 100,

            #endregion

            #region Auth 200

            EmptyEmail = 200,
            EmptyName = 201,
            EmptySurname = 202,
            EmptyPhone = 203,
            EmptyPassword = 204,
            EmailAlreadyUsed = 205,
            InvalidCredentials = 206,
            NotEmail = 207,
            NotPassword = 208,
            UserNotExist = 209,

            #endregion

            #region Data 300

            InvalidInput,

            #endregion
        };

        /// <summary>
        /// Object holding data about the exception such as the code or the message
        /// </summary>
        private class ExceptionData
        {
            /// <summary>
            /// COde that should be unique for all exceptions
            /// </summary>
            public readonly string Code;

            /// <summary>
            /// Message for given exception that might be shown to the user
            /// </summary>
            public readonly string Message;

            public ExceptionData(string code, string message)
            {
                Code = code;
                Message = message;
            }
        }

        /// <summary>
        /// Dictionary pairing all exception types with its data 
        /// </summary>
        private static Dictionary<ExceptionType, ExceptionData> _exceptions =
            new Dictionary<ExceptionType, ExceptionData>()
            {
                #region Base 100

                {ExceptionType.UnknownException, new ExceptionData("unknown", "Nastala neznámá chyba")},
                
                #endregion

                #region Auth 200

                {ExceptionType.EmptyEmail, new ExceptionData("emptyEmail", "Email nesmí být prázdný")},
                {ExceptionType.EmptyName, new ExceptionData("emptyName", "Jméno nesmí být prázdné")},
                {ExceptionType.EmptyPassword, new ExceptionData("emptyPassword", "Heslp nesmí být prázdné")},
                {ExceptionType.EmailAlreadyUsed, new ExceptionData("emailAlreadyUsed", "Email je již používán")},
                {ExceptionType.InvalidCredentials, new ExceptionData("invalidCredentials", "Neplatné přihlašovací údaje")},
                {ExceptionType.NotEmail, new ExceptionData("notEmail", "Email není platný")},
                {ExceptionType.NotPassword, new ExceptionData("notPassowrd", "Heslo neodpovídá požadavkům")},
                {ExceptionType.UserNotExist, new ExceptionData("userNotExist", "Požadovaný uživatel neexistuje")},

                #endregion

                #region Data 300

                {ExceptionType.InvalidInput, new ExceptionData("invalidInput", "Vstup není platný")},

                #endregion
            };

        
        /// <inheritdoc />
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="type"></param>
        public KarolinkaException(ExceptionType type)
        {
            var exceptionData = _exceptions[type];

            Code = exceptionData.Code;
            Message = exceptionData.Message;
        }
    }
    
}