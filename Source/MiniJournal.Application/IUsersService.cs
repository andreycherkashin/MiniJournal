using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Contracts.UsersApplicationService;
using Infotecs.MiniJournal.Domain.Users;

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
