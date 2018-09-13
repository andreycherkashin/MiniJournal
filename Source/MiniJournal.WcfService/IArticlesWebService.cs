using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.Contracts.ImagesApplicationsService;
using Infotecs.MiniJournal.Contracts.UsersApplicationService;

namespace Infotecs.MiniJournal.WcfService
{
    /// <summary>
    /// Интерфейс для веб сервиса.
    /// </summary>
    [ServiceContract]
    public interface IArticlesWebService
    {
        /// <summary>
        /// Возвращает список всех имеющихся статей.
        /// </summary>
        /// <returns>Список всех статей.</returns>
        /// <param name="request">Объект запроса.</param>
        [OperationContract]
        Task<GetArticlesResponse> GetArticlesAsync(GetArticlesRequest request);

        /// <summary>
        /// Возвращает одну статью.
        /// </summary>
        /// <param name="request">Объект запроса.</param>
        /// <returns>Запрошенную статей.</returns>     
        [OperationContract]
        Task<GetArticleResponse> GetArticleAsync(GetArticleRequest request);

        /// <summary>
        /// Создать статью с указанным содержимым.
        /// </summary>
        /// <param name="request">Объект запроса.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [OperationContract]
        Task<CreateArticleResponse> CreateArticleAsync(CreateArticleRequest request);

        /// <summary>
        /// Удаляет статью.
        /// </summary>
        /// <param name="request">Объект запроса.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [OperationContract]
        Task<DeleteArticleResponse> DeleteArticleAsync(DeleteArticleRequest request);

        /// <summary>
        /// Добавляет комментарий к статье.
        /// </summary>
        /// <param name="request">Объект запроса.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [OperationContract]
        Task<AddCommentResponse> AddCommentAsync(AddCommentRequest request);

        /// <summary>
        /// Удаляет комментарий.
        /// </summary>
        /// <param name="request">Объект запроса.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [OperationContract]
        Task<DeleteCommentResponse> DeleteCommentAsync(DeleteCommentRequest request);

        /// <summary>
        /// Находит картинку по идентификатору.        
        /// </summary>
        /// <param name="request">Объект запроса.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [OperationContract]
        Task<FindImageResponse> FindImageAsync(FindImageRequest request);

        /// <summary>
        /// Получить пользователя по имени.
        /// <exception cref="Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException">
        /// Если пользователь с таким именем не найден будем выброшено исключение
        /// <see cref="Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException"/>.
        /// </exception>
        /// </summary>
        /// <param name="request">Объект запроса.</param>
        /// <returns>Найденный пользователь.</returns>
        [OperationContract]
        Task<GetUserByNameResponse> GetUserByNameAsync(GetUserByNameRequest request);


        /// <summary>
        /// Добавляет нового пользователя с указанным именем.
        /// </summary>
        /// <param name="request">Объект запроса.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [OperationContract]
        Task<CreateNewUserResponse> CreateNewUserAsync(CreateNewUserRequest request);
    }
}
