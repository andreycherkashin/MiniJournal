using System;

namespace Infotecs.MiniJournal.Domain.Articles.Exceptions
{
    /// <inheritdoc/>
    public class ArticleNotFoundException : Exception
    {        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Infotecs.MiniJournal.Domain.Articles.Exceptions.ArticleNotFoundException" /> class.
        /// </summary>
        public ArticleNotFoundException()
            : base("Article not found.")
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Infotecs.MiniJournal.Domain.Articles.Exceptions.ArticleNotFoundException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>        
        public ArticleNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Infotecs.MiniJournal.Domain.Articles.Exceptions.ArticleNotFoundException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified. </param>
        public ArticleNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
