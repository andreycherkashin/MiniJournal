using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Users.Exceptions;

namespace Infotecs.MiniJournal.Domain.Users
{
    /// <summary>
    /// Действия над пользователем.
    /// </summary>
    public interface IUserDomainService
    {
        /// <summary>
        /// Создает пользователя.
        /// </summary>
        /// <exception cref="UserNotFoundException">
        /// Если пользователь не найден.
        /// </exception>
        /// <param name="user">Пользователь.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task CreateUserAsync(User user);

        /// <summary>
        /// Находит пользователя по имени.
        /// </summary>
        /// <exception cref="EmptyUserNameException">
        /// Если имя пользователя является пустой строкой или null.
        /// </exception>
        /// <exception cref="UserNotFoundException">
        /// Если пользователь не найден.
        /// </exception>
        /// <param name="name">Имя пользователя.</param>
        /// <returns>Пользователя.</returns>
        Task<User> GetUserByNameAsync(string name);

        /// <summary>
        /// Находит пользователя по идентификатору.
        /// </summary>
        /// <exception cref="UserNotFoundException">
        /// Если пользователь не найден.
        /// </exception>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Пользователя.</returns>
        Task<User> GetUserByIdAsync(long userId);
    }
}
