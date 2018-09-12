using System;

namespace Infotecs.MiniJournal.Domain.Users.Exceptions
{
    /// <inheritdoc/>
    public class DuplicateUserNameException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Infotecs.MiniJournal.Domain.Articles.Exceptions.DuplicateUserNameException" /> class.
        /// </summary>
        public DuplicateUserNameException()
            : base("User with such name already exists.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Infotecs.MiniJournal.Domain.Articles.Exceptions.DuplicateUserNameException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        public DuplicateUserNameException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Infotecs.MiniJournal.Domain.Articles.Exceptions.DuplicateUserNameException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified. </param>
        public DuplicateUserNameException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
