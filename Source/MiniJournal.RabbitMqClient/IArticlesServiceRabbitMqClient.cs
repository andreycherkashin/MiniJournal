using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Contracts.UsersApplicationService;

namespace Infotecs.MiniJournal.RabbitMqClient
{
    public interface IArticlesServiceRabbitMqClient
    {
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

        /// <summary>
        /// Получить пользователя по имени.
        /// <exception cref="Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException">
        /// Если пользователь с таким именем не найден будем выброшено исключение <see cref="Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException"/>. 
        /// </exception>
        /// </summary>
        /// <param name="request">Имя пользователя.</param>
        /// <returns>Найденный пользователь.</returns>
        Task<GetUserByNameResponse> GetUserByNameAsync(GetUserByNameRequest request);

        /// <summary>
        /// Добавляет нового пользователя с указанным именем.
        /// </summary>
        /// <param name="request">Имя пользователя.</param>
        Task<CreateNewUserResponse> CreateNewUserAsync(CreateNewUserRequest request);
    }
}
