using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Articles.Exceptions;
using Infotecs.MiniJournal.Domain.Comments;

namespace Infotecs.MiniJournal.Domain.Articles
{
    internal class ArticleDomainService : IArticleDomainService
    {
        private readonly IArticleRepository articleRepository;

        public ArticleDomainService(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public async Task CreateArticleAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));

            await this.articleRepository.AddAsync(article);
        }

        public async Task DeleteArticleAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));

            await this.articleRepository.DeleteAsync(article);
        }

        public async Task<Article> GetArticleByIdAsync(long articleId)
        {
            var article = await this.articleRepository.FindByIdAsync(articleId);

            if (article == null)
            {
                throw new ArticleNotFoundException();
            }

            return article;
        }
    }
}
