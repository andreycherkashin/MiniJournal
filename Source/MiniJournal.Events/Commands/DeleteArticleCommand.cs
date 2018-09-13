using System;

namespace Infotecs.MiniJournal.Events.Commands
{
    /// <summary>
    /// Удалить статью.
    /// </summary>
    public class DeleteArticleCommand : ICommand
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteArticleCommand"/> class.
        /// </summary>
        public DeleteArticleCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteArticleCommand"/> class.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        public DeleteArticleCommand(long articleId)
        {
            this.ArticleId = articleId;
        }

        /// <summary>
        /// Gets or sets идентификатор статьи.
        /// </summary>
        public long ArticleId { get; set; }
    }
}
