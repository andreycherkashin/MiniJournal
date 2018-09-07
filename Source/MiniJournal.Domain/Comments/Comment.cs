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
        /// Создает комментарий
        /// </summary>
        /// <param name="user">Пользователь создавший комментарий.</param>
        /// <param name="articleId"> Идентификатор статьи, к который был написан комментарий.</param>
        /// <param name="text">Содержимое комментария.</param>
        internal Comment(User user, long articleId, string text)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));            

            this.Text = text;
            this.User = user;            

            this.UserId = user.Id;
            this.ArticleId = articleId;
        }

        /// <summary>
        /// Уникальный идентификатор комментария.
        /// </summary>
        public long Id
        {
            get;
            // internal для ORM.
            internal set;
        }

        /// <summary>
        /// Содержимое комментария.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Пользователь создавший комментарий.
        /// </summary>
        public User User { get; private set; }



        /// <summary>
        /// Для ORM.
        /// Идентификатор пользователя, который написал комментарий.
        /// </summary>
        internal long UserId { get; private set; }

        /// <summary>
        /// Для ORM.
        /// Идентификатор статьи, к которой был написан комментарий.
        /// </summary>
        internal long ArticleId { get; private set; }
    }
}
