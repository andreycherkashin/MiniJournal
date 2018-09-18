using System;
using System.Threading.Tasks;

namespace Infotecs.MiniJournal.Domain.Comments
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем комментариев.
    /// </summary>
    public interface ICommentRepository
    {
        /// <summary>
        /// Находит комментарий по идентификатору. Если комментарий не найден, возвращается null.
        /// </summary>
        /// <param name="commentId">Идентификатор комментария.</param>
        /// <returns>Комментарий, либо null, если не найден.</returns>
        Task<Comment> FindByIdAsync(long commentId);

        /// <summary>
        /// Добавляет комментарий.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <param name="comment">Комментарий.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task AddAsync(long articleId, Comment comment);

        /// <summary>
        /// Удаляет комментарий.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <param name="comment">Комментарий.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task DeleteAsync(long articleId, Comment comment);
    }
}
