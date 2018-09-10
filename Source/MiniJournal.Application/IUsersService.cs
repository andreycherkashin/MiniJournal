using System;
using System.Threading.Tasks;
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
        /// <param name="name">Имя пользователя.</param>
        /// <returns>Найденный пользователь.</returns>
        Task<User> GetUserByNameAsync(string name);

        /// <summary>
        /// Добавляет нового пользователя с указанным именем.
        /// </summary>
        /// <param name="name">Имя пользователя.</param>
        Task CreateNewUserAsync(string name);
    }
}
