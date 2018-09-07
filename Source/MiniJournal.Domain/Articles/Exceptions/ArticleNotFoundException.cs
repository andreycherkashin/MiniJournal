using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infotecs.MiniJournal.Domain.Articles.Exceptions
{
    /// <inheritdoc />    
    public class ArticleNotFoundException : Exception
    {
        public ArticleNotFoundException()
            : base("Article not found.")
        {
        }

        public ArticleNotFoundException(string message)
            : base(message)
        {
        }
        
        public ArticleNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
