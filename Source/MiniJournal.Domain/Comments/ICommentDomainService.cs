using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Comments.Exceptions;

namespace Infotecs.MiniJournal.Domain.Comments
{
    /// <summary>
    /// Действия над комментарием.
    /// </summary>
    public interface ICommentDomainService
    {
        /// <summary>
        /// Добавляет комментарий к статье.
        /// </summary>
        /// <param name="article">Статья.</param>
        /// <param name="comment">Комментарий.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task AddCommentAsync(Article article, Comment comment);

        /// <summary>
        /// Удаляет комментарий.
        /// </summary>
        /// <param name="article">Статья.</param>
        /// <param name="comment">Комментарий.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task DeleteCommentAsync(Article article, Comment comment);

        /// <summary>
        /// Возвращает комментарий по идентификатору.
        /// </summary>
        /// <exception cref="CommentNotFoundException">
        /// Если комментарий я таким идентификатором не найден.
        /// </exception>
        /// <param name="article">Статья.</param>
        /// <param name="commentId">Идентификатор комментария.</param>
        /// <returns>Комментарий.</returns>
        Task<Comment> GetCommentById(Article article, long commentId);
    }
}
