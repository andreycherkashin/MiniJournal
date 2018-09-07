using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infotecs.MiniJournal.Domain.Comments.Exceptions
{
    /// <inheritdoc />
    public class CommentNotFoundException : Exception
    {
        public CommentNotFoundException()
            : base("Comment not found.")
        {
        }
        
        public CommentNotFoundException(string message)
            : base(message)
        {
        }

        public CommentNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
