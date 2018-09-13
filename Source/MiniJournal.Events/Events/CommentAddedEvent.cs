using System;

namespace Infotecs.MiniJournal.Events.Events
{
    /// <summary>
    /// Событие добавления комментария.
    /// </summary>
    public class CommentAddedEvent : IEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentAddedEvent"/> class.
        /// </summary>
        public CommentAddedEvent()
        {
            this.DateOfCreate = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentAddedEvent"/> class.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <param name="commentId">Идентификатор комментария.</param>
        public CommentAddedEvent(long articleId, long commentId)
            : this()
        {
            this.ArticleId = articleId;
            this.CommentId = commentId;
        }

        /// <summary>
        /// Идентификатор статьи.
        /// </summary>
        public long ArticleId { get; set; }

        /// <summary>
        /// Идентификатор комментария.
        /// </summary>
        public long CommentId { get; set; }

        /// <inheritdoc />
        public DateTime DateOfCreate { get; }
    }
}
