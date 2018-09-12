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
        public Comment()
        {
        }

        /// <summary>
        /// Создает комментарий
        /// </summary>
        /// <param name="user">Пользователь создавший комментарий.</param>
        /// <param name="article">Статьи, к который был написан комментарий.</param>
        /// <param name="text">Содержимое комментария.</param>
        public Comment(User user, Article article, string text)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));            

            this.Text = text;
            this.User = user;
            this.Article = article;
        }

        /// <summary>
        /// Уникальный идентификатор комментария.
        /// </summary>
        public virtual long Id { get; protected internal set;}

        /// <summary>
        /// Содержимое комментария.
        /// </summary>
        public virtual string Text { get; set; }

        /// <summary>
        /// Пользователь создавший комментарий.
        /// </summary>
        public virtual User User { get; protected set; }

        /// <summary>
        /// Статья
        /// </summary>
        public virtual Article Article { get; protected set; }
    }
}
