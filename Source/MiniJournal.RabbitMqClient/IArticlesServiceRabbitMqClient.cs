using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.Contracts.UsersApplicationService;

namespace Infotecs.MiniJournal.RabbitMqClient
{
    /// <summary>
    /// Класс для связи с брокером сообщений и создания сообщений в очереди.
    /// </summary>
    public interface IArticlesServiceRabbitMqClient
    {
        /// <summary>
        /// Помещает запрос о создании статьи с указанным содержимым в очередь.
        /// </summary>
        /// <param name="request"><see cref="CreateArticleRequest"/>.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task CreateArticleAsync(CreateArticleRequest request);

        /// <summary>
        /// Помещает запрос об удалении статьи в очередь.
        /// </summary>
        /// <param name="request"><see cref="DeleteArticleRequest"/>.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task DeleteArticleAsync(DeleteArticleRequest request);

        /// <summary>
        /// Помещает запрос о добавлении комментария к статье в очередь.
        /// </summary>
        /// <param name="request"><see cref="AddCommentRequest"/>.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task AddCommentAsync(AddCommentRequest request);

        /// <summary>
        /// Помещает запрос об удалении комментария в очередь.
        /// </summary>
        /// <param name="request"><see cref="DeleteCommentRequest"/>.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task DeleteCommentAsync(DeleteCommentRequest request);

        /// <summary>
        /// Помещает запрос о получении пользователя по имени в очередь.
        /// <exception cref="Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException">
        /// Если пользователь с таким именем не найден будем выброшено исключение
        /// <see cref="Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException"/>.
        /// </exception>
        /// </summary>
        /// <param name="request">Имя пользователя.</param>
        /// <returns>Найденный пользователь.</returns>
        Task<GetUserByNameResponse> GetUserByNameAsync(GetUserByNameRequest request);

        /// <summary>
        /// Помещает запрос о добавлении нового пользователя с указанным именем в очередь.
        /// </summary>
        /// <param name="request"><see cref="CreateNewUserRequest"/>.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task CreateNewUserAsync(CreateNewUserRequest request);
    }
}
