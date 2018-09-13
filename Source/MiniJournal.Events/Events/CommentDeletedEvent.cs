using System;

namespace Infotecs.MiniJournal.Events.Events
{
    /// <summary>
    /// Событие удаления комментария.
    /// </summary>
    public class CommentDeletedEvent : IEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentDeletedEvent"/> class.
        /// </summary>
        public CommentDeletedEvent()
        {
            this.DateOfCreate = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentDeletedEvent"/> class.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <param name="commentId">Идентификатор комментария.</param>
        public CommentDeletedEvent(long articleId, long commentId)
            : this()
        {
            this.ArticleId = articleId;
            this.CommentId = commentId;
        }

        /// <summary>
        /// Идентификатор статьи.
        /// </summary>
        public long ArticleId { get; set;  }

        /// <summary>
        /// Идентификатор комментария.
        /// </summary>
        public long CommentId { get; set; }

        /// <inheritdoc />
        public DateTime DateOfCreate { get; }
    }
}
