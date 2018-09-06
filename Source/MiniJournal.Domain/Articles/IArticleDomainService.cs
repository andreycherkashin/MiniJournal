using System.Threading.Tasks;

namespace Infotecs.MiniJournal.Domain.Articles
{
    public interface IArticleDomainService
    {
        Task CreateArticleAsync(Article article);
        Task DeleteArticleAsync(Article article);
        Task<Article> GetArticleByIdAsync(long articleId);
    }
}