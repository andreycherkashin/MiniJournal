using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Users.Exceptions;

namespace Infotecs.MiniJournal.Domain.Users
{
    /// <inheritdoc/>
    /// <summary>
    /// Действия над пользователем.
    /// </summary>
    internal class UserDomainService : IUserDomainService
    {
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDomainService"/> class.
        /// </summary>
        /// <param name="userRepository"><see cref="IUserRepository"/>IUserRepository.</param>
        public UserDomainService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Находит пользователя по имени.
        /// </summary>
        /// <exception cref="T:Infotecs.MiniJournal.Domain.Users.Exceptions.EmptyUserNameException">
        /// Если имя пользователя является пустой строкой или null.
        /// </exception>
        /// <exception cref="T:Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException">
        /// Если пользователь не найден.
        /// </exception>
        /// <param name="name">Имя пользователя.</param>
        /// <returns>Пользователя.</returns>
        public async Task<User> GetUserByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new EmptyUserNameException();
            }

            User user = await this.userRepository.FindByNameAsync(name);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Находит пользователя по идентификатору.
        /// </summary>
        /// <exception cref="T:Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException">
        /// Если пользователь не найден.
        /// </exception>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Пользователя.</returns>
        public async Task<User> GetUserByIdAsync(long userId)
        {
            User user = await this.userRepository.FindByIdAsync(userId);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Создает пользователя.
        /// </summary>
        /// <exception cref="T:Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException">
        /// Если пользователь не найден.
        /// </exception>
        /// <param name="user">Пользователь.</param>
        public async Task CreateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (await this.userRepository.FindByNameAsync(user.Name) != null)
            {
                throw new DuplicateUserNameException();
            }

            await this.userRepository.AddAsync(user);
        }
    }
}
