using System;

namespace Infotecs.MiniJournal.Domain.Users.Exceptions
{
    /// <inheritdoc/>
    public class EmptyUserNameException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Infotecs.MiniJournal.Domain.Articles.Exceptions.EmptyUserNameException" /> class.
        /// </summary>
        public EmptyUserNameException()
            : base("User name should not be empty.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Infotecs.MiniJournal.Domain.Articles.Exceptions.EmptyUserNameException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        public EmptyUserNameException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Infotecs.MiniJournal.Domain.Articles.Exceptions.EmptyUserNameException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified. </param>
        public EmptyUserNameException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
