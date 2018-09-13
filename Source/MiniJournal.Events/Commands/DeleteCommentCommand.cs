using System;

namespace Infotecs.MiniJournal.Events.Commands
{
    /// <summary>
    /// Удалить комментарий.
    /// </summary>
    public class DeleteCommentCommand : ICommand
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
