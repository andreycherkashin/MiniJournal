using System;

namespace Infotecs.MiniJournal.Contracts.Commands.ArticlesApplicationService
{
    /// <summary>
    /// Запрос удаления статьи.
    /// </summary>
    public class DeleteArticleRequest : ICommand
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteArticleRequest"/> class.
        /// </summary>
        public DeleteArticleRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteArticleRequest"/> class.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        public DeleteArticleRequest(long articleId)
        {
            this.ArticleId = articleId;
        }

        /// <summary>
        /// Gets or sets идентификатор статьи.
        /// </summary>
        public long ArticleId { get; set; }
    }
}
