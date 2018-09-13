using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Articles.Exceptions;

namespace Infotecs.MiniJournal.Domain.Articles
{
    /// <inheritdoc/>
    /// <summary>
    /// Класс для работы со статьей.
    /// </summary>
    internal class ArticleDomainService : IArticleDomainService
    {
        private readonly IArticleRepository articleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleDomainService"/> class.
        /// </summary>
        /// <param name="articleRepository"><see cref="IArticleRepository"/>.</param>
        public ArticleDomainService(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Создает статью.
        /// </summary>
        /// <param name="article">Статья.</param>
        public async Task CreateArticleAsync(Article article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            await this.articleRepository.AddAsync(article);
        }

        /// <inheritdoc/>
        /// <summary>
        /// Удаляет статью.
        /// </summary>
        /// <param name="article">Статья.</param>
        public async Task DeleteArticleAsync(Article article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            await this.articleRepository.DeleteAsync(article);
        }

        /// <inheritdoc/>
        /// <summary>
        /// Возвращает статью по идентификатору.
        /// </summary>
        /// <exception cref="T:Infotecs.MiniJournal.Domain.Articles.Exceptions.ArticleNotFoundException">
        /// Если статья не найдена.
        /// </exception>
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <returns>Найденную статью.</returns>
        public async Task<Article> GetArticleByIdAsync(long articleId)
        {
            Article article = await this.articleRepository.FindByIdAsync(articleId);

            if (article == null)
            {
                throw new ArticleNotFoundException();
            }

            return article;
        }
    }
}
