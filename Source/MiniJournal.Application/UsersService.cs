using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Users;

namespace Infotecs.MiniJournal.Application
{
    /// <summary>
    /// Реализует высокоуровненвый интерфейс для работы с пользователями.
    /// </summary>
    internal class UsersService : IUsersService
    {
        private readonly IUserDomainService userService;
        private readonly IUserFactory userFactory;

        public UsersService(
            IUserDomainService userService,
            IUserFactory userFactory)
        {
            this.userService = userService;
            this.userFactory = userFactory;
        }

        /// <summary>
        /// Получить пользователя по имени.
        /// <exception cref="Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException">
        /// Если пользователь с таким именем не найден будем выброшено исключение <see cref="Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException"/>. 
        /// </exception>
        /// </summary>
        /// <param name="name">Имя пользователя.</param>
        /// <returns>Найденный пользователь.</returns>
        public Task<User> GetUserByNameAsync(string name)
        {
            return this.userService.GetUserByNameAsync(name);
        }

        /// <summary>
        /// Добавляет нового пользователя с указанным именем.
        /// </summary>
        /// <param name="name">Имя пользователя.</param>
        public async Task CreateNewUserAsync(string name)
        {
            var user = await this.userFactory.CreateAsync(name);

            await this.userService.CreateUserAsync(user);
        }
    }
}
