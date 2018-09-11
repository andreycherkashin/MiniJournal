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
        protected Article()
        {
            
        }

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
        }

        /// <summary>
        /// Уникальный идентификатор статьи
        /// </summary>
        public virtual long Id { get; protected internal set; }

        /// <summary>
        /// Содержимое статьи
        /// </summary>
        public virtual string Text { get; set; }

        /// <summary>
        /// Уникальный идентификатор картинки 
        /// </summary>
        public virtual string ImageId { get; protected internal set; }

        /// <summary>
        /// Пользователь, создавший статью.
        /// </summary>
        public virtual User User { get; protected set; }

        /// <summary>
        /// Список комментариев к статье.
        /// </summary>
        public virtual ICollection<Comment> Comments { get; protected set; }
    }
}
