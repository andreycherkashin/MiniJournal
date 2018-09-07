using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infotecs.MiniJournal.Domain.Users
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем пользователей.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Находит пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Пользователя, либо null, если не найден.</returns>
        Task<User> FindByIdAsync(long id);

        /// <summary>
        /// Находит пользователя по имени.
        /// </summary>
        /// <param name="name">Имя пользователя.</param>
        /// <returns>Пользователя, либо null, если не найден.</returns>
        Task<User> FindByNameAsync(string name);

        /// <summary>
        /// Добавляет пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        Task AddAsync(User user);
    }
}
