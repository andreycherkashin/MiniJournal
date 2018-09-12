using System;
using System.Collections.Generic;
using Infotecs.MiniJournal.Domain.Comments;
using Infotecs.MiniJournal.Domain.Users;

namespace Infotecs.MiniJournal.Domain.Articles
{
    /// <summary>
    /// Статья с комментариями.
    /// </summary>
    public class Article
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Article"/> class.
        /// </summary>
        /// <param name="user">Пользователь создавший статью.</param>
        /// <param name="text">Содержимое статьи.</param>
        /// <param name="comments">Комментарии, если есть.</param>
        public Article(User user, string text, List<Comment> comments = null)
        {
            this.Text = text;
            this.User = user ?? throw new ArgumentNullException(nameof(user));

            this.Comments = comments ?? new List<Comment>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Article"/> class.
        /// </summary>
        protected Article()
        {
        }

        /// <summary>
        /// Gets or sets уникальный идентификатор статьи.
        /// </summary>
        public virtual long Id { get; protected internal set; }

        /// <summary>
        /// Gets or sets содержимое статьи.
        /// </summary>
        public virtual string Text { get; set; }

        /// <summary>
        /// Gets or sets уникальный идентификатор картинки.
        /// </summary>
        public virtual string ImageId { get; protected internal set; }

        /// <summary>
        /// Gets or sets пользователь, создавший статью.
        /// </summary>
        public virtual User User { get; protected set; }

        /// <summary>
        /// Gets or sets список комментариев к статье.
        /// </summary>
        public virtual ICollection<Comment> Comments { get; protected set; }
    }
}
