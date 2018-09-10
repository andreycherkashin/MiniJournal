using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    /// <summary>
    /// Запрос удаления статьи.
    /// </summary>
    public class DeleteArticleRequest
    {
        /// <summary>
        /// Идентификатор статьи.
        /// </summary>
        public long ArticleId { get; set; }
    }
}
