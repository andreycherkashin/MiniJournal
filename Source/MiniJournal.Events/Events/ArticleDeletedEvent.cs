using System;

namespace Infotecs.MiniJournal.Events.Events
{
    /// <summary>
    /// Событие удаления статьи.
    /// </summary>
    public class ArticleDeletedEvent : IEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleDeletedEvent"/> class.
        /// </summary>
        public ArticleDeletedEvent()
        {
            this.DateOfCreate = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleDeletedEvent"/> class.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        public ArticleDeletedEvent(long articleId)
            : this()
        {
            this.ArticleId = articleId;
        }

        /// <summary>
        /// Идентификатор статьи.
        /// </summary>
        public long ArticleId { get; set; }

        /// <inheritdoc />
        public DateTime DateOfCreate { get; set; }
    }
}
