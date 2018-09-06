using System;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Users;

namespace Infotecs.MiniJournal.Domain.Comments
{
    public class Comment
    {
        public Comment(User user, Article article, string text)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (article == null)
                throw new ArgumentNullException(nameof(article));

            this.Text = text;
            this.User = user;
            this.Article = article;

            this.UserId = user.Id;
            this.ArticleId = article.Id;
        }

        public long Id { get; private set; }
        public string Text { get; set; }
        public User User { get; set; }
        public Article Article { get; set; }

        public long UserId { get; set; }
        public long ArticleId { get; set; }
    }
}
