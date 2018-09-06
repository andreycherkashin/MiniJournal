using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Users;

namespace Infotecs.MiniJournal.Domain.Comments
{
    internal class CommentFactory : ICommentFactory
    {
        public Task<Comment> CreateAsync(string text, User user, Article article)
        {
            var comment = new Comment(user, article, text);

            return Task.FromResult(comment);
        }
    }
}
