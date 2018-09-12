using System;
using System.Threading.Tasks;

namespace Infotecs.MiniJournal.Domain.Users
{
    /// <summary>
    /// Инкапсулирует процесс и способ создания пользователей.
    /// </summary>
    public interface IUserFactory
    {
        /// <summary>
        /// Создает пользователя.
        /// </summary>
        /// <param name="name">Имя пользователя.</param>
        /// <returns>Пользователя.</returns>
        Task<User> CreateAsync(string name);
    }
}
