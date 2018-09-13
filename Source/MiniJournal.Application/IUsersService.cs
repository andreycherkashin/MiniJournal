using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Contracts.Commands.UsersApplicationService;

namespace Infotecs.MiniJournal.Application
{
    /// <summary>
    /// Реализует высокоуровненвый интерфейс для работы с пользователями.
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// Получить пользователя по имени.
        /// <exception cref="Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException">
        /// Если пользователь с таким именем не найден будем выброшено исключение
        /// <see cref="Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException"/>.
        /// </exception>
        /// </summary>
        /// <param name="request">Имя пользователя.</param>
        /// <returns>Найденный пользователь.</returns>
        Task<GetUserByNameResponse> GetUserByNameAsync(GetUserByNameRequest request);

        /// <summary>
        /// Добавляет нового пользователя с указанным именем.
        /// </summary>
        /// <param name="request">Имя пользователя.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<CreateNewUserResponse> CreateNewUserAsync(CreateNewUserRequest request);
    }
}
