using System;

namespace Infotecs.MiniJournal.Contracts.Events
{
    /// <summary>
    /// Событие создания статьи.
    /// </summary>
    public class ArticleCreatedEvent : IEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleCreatedEvent"/> class.
        /// </summary>
        public ArticleCreatedEvent()
        {
            this.DateOfCreate = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleCreatedEvent"/> class.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        public ArticleCreatedEvent(long articleId)
            : this()
        {
            this.ArticleId = articleId;
        }

        /// <inheritdoc />
        public DateTime DateOfCreate { get; }

        /// <summary>
        /// Идентификатор статьи.
        /// </summary>
        public long ArticleId { get; set; }
    }
}
