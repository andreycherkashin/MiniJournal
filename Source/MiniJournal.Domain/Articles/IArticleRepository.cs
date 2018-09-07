using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infotecs.MiniJournal.Domain.Articles
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем комментариев.
    /// </summary>
    public interface IArticleRepository
    {
        /// <summary>
        /// Возвращает список имеющихся статей.
        /// </summary>
        /// <returns>Список статей.</returns>
        Task<IEnumerable<Article>> GetArticlesAsync();

        /// <summary>
        /// Находит статью по идентификатору. Если статья не найдена, возвращается null.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <returns>Статью, либо null, если не найдена.</returns>
        Task<Article> FindByIdAsync(long articleId);

        /// <summary>
        /// Удаляет статью.
        /// </summary>
        /// <param name="article">Статья.</param>
        Task DeleteAsync(Article article);

        /// <summary>
        /// Добавляет статью.
        /// </summary>
        /// <param name="article">Статья.</param>
        Task AddAsync(Article article);
    }
}
