using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    /// <summary>
    /// Запрос комментария.
    /// </summary>
    public class GetCommentRequest
    {
        /// <summary>
        /// Идентификатор комментария.
        /// </summary>
        public long CommentId { get; set; }
    }
}
