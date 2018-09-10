using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService.Entites;
using Infotecs.MiniJournal.Contracts.ImagesApplicationsService;
using Infotecs.MiniJournal.Contracts.UsersApplicationService;
using Infotecs.MiniJournal.Contracts.UsersApplicationService.Entities;

namespace Infotecs.MiniJournal.WcfService
{
    [ServiceContract]
    public interface IArticlesWebService
    {
        /// <summary>
        /// Возвращает список всех имеющихся статей
        /// </summary>
        /// <returns>Список всех статей</returns>
        [OperationContract]
        Task<GetArticlesResponse> GetArticlesAsync(GetArticlesRequest request);

        /// <summary>
        /// Создать статью с указанным содержимым.
        /// </summary>
        [OperationContract]
        Task<CreateArticleResponse> CreateArticleAsync(CreateArticleRequest request);

        /// <summary>
        /// Удаляет статью.
        /// </summary>
        [OperationContract]
        Task<DeleteArticleResponse> DeleteArticleAsync(DeleteArticleRequest request);

        /// <summary>
        /// Добавляет комментарий к статье.
        /// </summary>
        [OperationContract]
        Task<AddCommentResponse> AddCommentAsync(AddCommentRequest request);

        /// <summary>
        /// Удаляет комментарий.
        /// </summary>
        [OperationContract]
        Task<DeleteCommentResponse> DeleteCommentAsync(DeleteCommentRequest request);

        /// <summary>
        /// Находит картинку по идентификатору.
        /// </summary>
        [OperationContract]
        Task<FindImageResponse> FindImageAsync(FindImageRequest request);

        /// <summary>
        /// Получить пользователя по имени.
        /// <exception cref="Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException">
        /// Если пользователь с таким именем не найден будем выброшено исключение <see cref="Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException"/>. 
        /// </exception>
        /// </summary>        
        /// <returns>Найденный пользователь.</returns>
        [OperationContract]
        Task<GetUserByNameResponse> GetUserByNameAsync(GetUserByNameRequest request);

        /// <summary>
        /// Добавляет нового пользователя с указанным именем.
        /// </summary>
        [OperationContract]
        Task<CreateNewUserResponse> CreateNewUserAsync(CreateNewUserRequest request);
    }    
}
