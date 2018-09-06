using Infotecs.MiniJournal.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Articles;

namespace Infotecs.MiniJournal.Domain.Comments
{
    public interface ICommentFactory
    {
        Task<Comment> CreateAsync(string text, User user, Article article);
    }
}
