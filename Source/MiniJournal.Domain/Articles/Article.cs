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
        public Article(User user, string text, List<Comment> comments)
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
        public long Id { get; private set; }

        /// <summary>
        /// Содержимое статьи
        /// </summary>
        public string Text { get; set; }
        public string ImageId { get; set; }
        public User User { get; internal set; }
        public List<Comment> Comments { get; internal set; }

        public long UserId { get; internal set; }
    }
}
