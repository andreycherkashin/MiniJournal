using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    /// <summary>
    /// Запрос удаления комментария.
    /// </summary>
    public class DeleteCommentRequest
    {
        /// <summary>
        /// Идентификатор статьи.
        /// </summary>
        public long ArticleId { get; set; }

        /// <summary>
        /// Идентификатор комментария.
        /// </summary>
        public long CommentId { get; set; }
    }
}
