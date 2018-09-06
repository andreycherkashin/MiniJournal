using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Comments;
using Infotecs.MiniJournal.Domain.Users;

namespace Infotecs.MiniJournal.Domain.Articles
{
    internal class ArticleFactory : IArticleFactory
    {
        public Task<Article> CreateArticleAsync(string text, string imageId, User user)
        {
            var article = new Article(user, text, new List<Comment>());

            return Task.FromResult(article);
        }
    }
}
