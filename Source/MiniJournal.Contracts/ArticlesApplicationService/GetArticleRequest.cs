using System;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    /// <summary>
    /// Запрос одной статьи.
    /// </summary>
    public class GetArticleRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetArticleRequest"/> class.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        public GetArticleRequest(long articleId)
        {
            this.ArticleId = articleId;
        }

        /// <summary>
        /// Идентификатор статьи.
        /// </summary>
        public long ArticleId { get; set; }
    }
}
