using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infotecs.MiniJournal.Domain.Articles
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetArticlesAsync();
        Task<Article> FindByIdAsync(long articleId);
        Task DeleteAsync(Article article);
        Task AddAsync(Article article);
    }
}
