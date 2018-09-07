using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infotecs.MiniJournal.Domain.Users.Exceptions
{
    /// <inheritdoc/>
    public class DuplicateUserNameException : Exception
    {        
        public DuplicateUserNameException()
            : base("User with such name already exists.")
        {
        }
        
        public DuplicateUserNameException(string message) 
            : base(message)
        {
        }

        public DuplicateUserNameException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
