using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infotecs.MiniJournal.Domain.Comments
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем комментариев.
    /// </summary>
    public interface ICommentRepository
    {
        /// <summary>
        /// Добавляет комментарий.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <param name="comment">Комментарий.</param>
        Task AddAsync(long articleId, Comment comment);

        /// <summary>
        /// Удаляет комментарий.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <param name="comment">Комментарий.</param>
        Task DeleteAsync(long articleId, Comment comment);
    }
}
