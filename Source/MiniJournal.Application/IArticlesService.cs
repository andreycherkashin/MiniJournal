using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Comments;

namespace MiniJournal.Application
{
    /// <summary>
    /// Класс реализует различные высокоуровневые операции над статьей.
    /// </summary>
    public interface IArticlesService
    {
        /// <summary>
        /// Возвращает список всех имеющихся статей
        /// </summary>
        /// <returns>Список всех статей</returns>
        Task<IEnumerable<Article>> GetArticlesAsync();

        /// <summary>
        /// Создать статью с указанным содержимым.
        /// </summary>
        /// <param name="text">Содержимое статьи.</param>
        /// <param name="image">Картинка.</param>
        /// <param name="userId">Идентификатор пользователя создавшего статью.</param>
        Task CreateArticleAsync(string text, byte[] image, long userId);

        /// <summary>
        /// Удаляет статью.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        Task DeleteArticleAsync(long articleId);

        /// <summary>
        /// Добавляет комментарий к статье.
        /// </summary>
        /// <param name="text">Содержимое комментария.</param>
        /// <param name="userId">Идентификатор пользователя добавившего комментарий.</param>
        /// <param name="articleId">Идентификатор статьи, к которой добавляется комментарий.</param>
        Task AddCommentAsync(string text, long userId, long articleId);

        /// <summary>
        /// Удаляет комментарий.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <param name="commentId">Идентификатор комментария.</param>
        Task DeleteCommentAsync(long articleId, long commentId);
    }
}
