using System;

namespace Infotecs.MiniJournal.Contracts.Commands.ArticlesApplicationService
{
    /// <summary>
    /// Запрос удаления комментария.
    /// </summary>
    public class DeleteCommentRequest : ICommand
    {
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
