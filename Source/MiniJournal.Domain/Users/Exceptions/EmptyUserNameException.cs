using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infotecs.MiniJournal.Domain.Users.Exceptions
{
    /// <inheritdoc/>
    public class EmptyUserNameException : Exception
    {        
        public EmptyUserNameException()
            : base("User name should not be empty.")
        {
        }

        public EmptyUserNameException(string message)
            : base(message)
        {
        }

        public EmptyUserNameException(string message, Exception innerException)
            : base(message, innerException)
        {
        }        
    }
}
