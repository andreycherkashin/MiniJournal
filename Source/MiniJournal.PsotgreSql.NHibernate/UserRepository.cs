using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Users;
using NHibernate.Linq;

namespace Infotecs.MiniJournal.PostgreSql.NHibernate
{
    internal class UserRepository : BaseNHibernateRepository, IUserRepository
    {
        public UserRepository(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }

        /// <summary>
        /// Находит пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Пользователя, либо null, если не найден.</returns>
        public Task<User> FindByIdAsync(long id)
            => this.Session.GetAsync<User>(id);

        /// <summary>
        /// Находит пользователя по имени.
        /// </summary>
        /// <param name="name">Имя пользователя.</param>
        /// <returns>Пользователя, либо null, если не найден.</returns>
        public Task<User> FindByNameAsync(string name)
            => this.Session.Query<User>().FirstOrDefaultAsync(x => x.Name == name);

        /// <summary>
        /// Добавляет пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        public Task AddAsync(User user)
            => this.Session.SaveAsync(user);
    }
}
