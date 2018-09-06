using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Users;

namespace Infotecs.MiniJournal.Domain.Articles
{
    public interface IArticleFactory
    {
        Task<Article> CreateArticleAsync(string text, string imageId, User user);
    }
}
