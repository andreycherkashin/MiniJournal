using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;

namespace Infotecs.MiniJournal.Application
{
    /// <summary>
    /// Класс реализует различные высокоуровневые операции над статьей.
    /// </summary>
    public interface IArticlesService
    {
        /// <summary>
        /// Возвращает список всех имеющихся статей.
        /// </summary>
        /// <param name="request">Запрос списка статей.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<GetArticlesResponse> GetArticlesAsync(GetArticlesRequest request);

        /// <summary>
        /// Возвращает статью.
        /// </summary>
        /// <param name="request"><see cref="GetArticleRequest"/>.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<GetArticleResponse> GetArticleAsync(GetArticleRequest request);

        /// <summary>
        /// Возвращает статью.
        /// </summary>
        /// <param name="request"><see cref="GetCommentRequest"/>.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<GetCommentResponse> GetCommentAsync(GetCommentRequest request);

        /// <summary>
        /// Создать статью с указанным содержимым.
        /// </summary>
        /// <param name="request">Запрос создания статьи.</param>
        /// <exception cref="T:Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException">
        /// Если пользователь не найден.
        /// </exception>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<CreateArticleResponse> CreateArticleAsync(CreateArticleRequest request);

        /// <summary>
        /// Удаляет статью.
        /// </summary>        
        /// <param name="request">Запрос удаления статьи.</param>
        /// <exception cref="T:Infotecs.MiniJournal.Domain.Articles.Exceptions.ArticleNotFoundException">
        /// Если статья не найдена.
        /// </exception>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<DeleteArticleResponse> DeleteArticleAsync(DeleteArticleRequest request);

        /// <summary>
        /// Добавляет комментарий к статье.
        /// </summary>
        /// <param name="request">Запрос добавления статьи.</param>
        /// <exception cref="T:Infotecs.MiniJournal.Domain.Articles.Exceptions.ArticleNotFoundException">
        /// Если статья не найдена.
        /// </exception>
        /// <exception cref="T:Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException">
        /// Если пользователь не найден.
        /// </exception>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<AddCommentResponse> AddCommentAsync(AddCommentRequest request);

        /// <summary>
        /// Удаляет комментарий.
        /// </summary>
        /// <param name="request">Запрос удаления комментария.</param>
        /// <exception cref="T:Infotecs.MiniJournal.Domain.Articles.Exceptions.ArticleNotFoundException">
        /// Если статья не найдена.
        /// </exception>
        /// <exception cref="T:Infotecs.MiniJournal.Domain.Comments.Exceptions.CommentNotFoundException">
        /// Если комментарий не найден.
        /// </exception>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<DeleteCommentResponse> DeleteCommentAsync(DeleteCommentRequest request);
    }
}
