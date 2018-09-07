﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infotecs.MiniJournal.Domain.Users.Exceptions
{
    /// <inheritdoc />
    public class UserNotFoundException : Exception
    {        
        public UserNotFoundException()
            : base("User not found.")
        {
        }

        public UserNotFoundException(string message)
            : base(message)
        {
        }
        
        public UserNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
