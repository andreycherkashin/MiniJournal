using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Users.Exceptions;

namespace Infotecs.MiniJournal.Domain.Users
{
    /// <inheritdoc/>
    /// <summary>
    /// Инкапсулирует процесс и способ создания пользователей.
    /// </summary>
    internal class UserFactory : IUserFactory
    {
        /// <inheritdoc/>
        /// <summary>
        /// Создает пользователя.
        /// </summary>
        /// <param name="name">Имя пользователя.</param>
        /// <returns>Пользователя.</returns>
        public Task<User> CreateAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new EmptyUserNameException();
            }

            var user = new User(name);

            return Task.FromResult(user);
        }
    }
}
