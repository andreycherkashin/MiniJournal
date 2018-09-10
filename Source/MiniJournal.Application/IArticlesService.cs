using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.Domain.Articles;

namespace Infotecs.MiniJournal.Application
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
        Task<GetArticlesResponse> GetArticlesAsync(GetArticlesRequest request);

        /// <summary>
        /// Создать статью с указанным содержимым.
        /// </summary>
        /// <param name="request">Запрос создания статьи.</param>
        Task<CreateArticleResponse> CreateArticleAsync(CreateArticleRequest request);

        /// <summary>
        /// Удаляет статью.
        /// </summary>
        /// <param name="request">Запрос удаления статьи.</param>
        Task<DeleteArticleResponse> DeleteArticleAsync(DeleteArticleRequest request);

        /// <summary>
        /// Добавляет комментарий к статье.
        /// </summary>
        /// <param name="request">Запрос добавления статьи.</param>        
        Task<AddCommentResponse> AddCommentAsync(AddCommentRequest request);

        /// <summary>
        /// Удаляет комментарий.
        /// </summary>
        /// <param name="request">Запрос удаления комментария.</param>        
        Task<DeleteCommentResponse> DeleteCommentAsync(DeleteCommentRequest request);
    }
}
