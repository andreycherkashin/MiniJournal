using System;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    /// <summary>
    /// Запрос удаления комментария.
    /// </summary>
    public class DeleteCommentRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommentRequest"/> class.
        /// </summary>
        public DeleteCommentRequest()
        {   
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommentRequest"/> class.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <param name="commentId">Идентификатор комментария.</param>
        public DeleteCommentRequest(long articleId, long commentId)
        {
            this.ArticleId = articleId;
            this.CommentId = commentId;
        }

        /// <summary>
        /// Gets or sets идентификатор статьи.
        /// </summary>
        public long ArticleId { get; set; }

        /// <summary>
        /// Gets or sets идентификатор комментария.
        /// </summary>
        public long CommentId { get; set; }
    }
}
