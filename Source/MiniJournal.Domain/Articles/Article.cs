using System;
using System.Collections.Generic;
using Infotecs.MiniJournal.Domain.Comments;
using Infotecs.MiniJournal.Domain.Users;

namespace Infotecs.MiniJournal.Domain.Articles
{
    /// <summary>
    /// Статьи с комментариями
    /// </summary>
    public class Article
    {
        /// <summary>
        /// Создает статью.
        /// </summary>
        /// <param name="user">Пользователь создавший статью.</param>
        /// <param name="text">Содержимое статьи.</param>
        /// <param name="comments">Комментарии, если есть.</param>
        public Article(User user, string text, List<Comment> comments = null)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            this.Text = text;
            this.User = user;

            this.Comments = comments ?? new List<Comment>();

            this.UserId = user.Id;
        }

        /// <summary>
        /// Уникальный идентификатор статьи
        /// </summary>
        public long Id
        {
            get;
            // internal для ORM.
            internal set;
        }

        /// <summary>
        /// Содержимое статьи
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Уникальный идентификатор картинки 
        /// </summary>
        public string ImageId { get; set; }

        /// <summary>
        /// Пользователь, создавший статью.
        /// </summary>
        public User User { get; private set; }

        /// <summary>
        /// Список комментариев к статье.
        /// </summary>
        public List<Comment> Comments { get; private set; }



        /// <summary>
        /// Для ORM.
        /// Идентификатор пользователя статьи.
        /// </summary>
        public long UserId { get; private set; }
    }
}
