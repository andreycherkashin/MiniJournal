using System;

namespace Infotecs.MiniJournal.Contracts.Commands.ArticlesApplicationService
{
    /// <summary>
    /// Запрос одной статьи.
    /// </summary>
    public class GetArticleRequest
    {
        /// <summary>
        /// Идентификатор статьи.
        /// </summary>
        public long ArticleId { get; set; }
    }
}
