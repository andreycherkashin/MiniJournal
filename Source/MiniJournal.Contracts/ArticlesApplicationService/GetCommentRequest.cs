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
        /// Initializes a new instance of the <see cref="GetCommentRequest"/> class.
        /// </summary>
        public GetCommentRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCommentRequest"/> class.
        /// </summary>
        /// <param name="commentId">Идентификатор комментария.</param>
        public GetCommentRequest(long commentId)
        {
            this.CommentId = commentId;
        }

        /// <summary>
        /// Идентификатор комментария.
        /// </summary>
        public long CommentId { get; set; }
    }
}
