using System;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Users;

namespace Infotecs.MiniJournal.Domain.Comments
{
    /// <summary>
    /// Комментарий к статье.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        public Comment()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        /// <param name="user">Пользователь создавший комментарий.</param>
        /// <param name="article">Статьи, к который был написан комментарий.</param>
        /// <param name="text">Содержимое комментария.</param>
        public Comment(User user, Article article, string text)
        {
            this.Text = text;
            this.User = user ?? throw new ArgumentNullException(nameof(user));
            this.Article = article;
        }

        /// <summary>
        /// Уникальный идентификатор комментария.
        /// </summary>
        public virtual long Id { get; protected internal set; }

        /// <summary>
        /// Gets or sets содержимое комментария.
        /// </summary>
        public virtual string Text { get; set; }

        /// <summary>
        /// Gets or sets пользователь создавший комментарий.
        /// </summary>
        public virtual User User { get; protected set; }

        /// <summary>
        /// Gets or sets статья.
        /// </summary>
        public virtual Article Article { get; protected set; }
    }
}
