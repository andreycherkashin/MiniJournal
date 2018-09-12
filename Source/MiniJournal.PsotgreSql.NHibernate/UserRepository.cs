using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Users;
using NHibernate.Linq;

namespace Infotecs.MiniJournal.PostgreSql.NHibernate
{
    /// <inheritdoc cref="IUserRepository" />
    internal class UserRepository : BaseNHibernateRepository, IUserRepository
    {
        /// <inheritdoc cref="BaseNHibernateRepository" />
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
        {
            return this.Session.GetAsync<User>(id);
        }

        /// <inheritdoc />
        public Task<User> FindByNameAsync(string name)
        {
            return this.Session.Query<User>().FirstOrDefaultAsync(x => x.Name == name);
        }

        /// <inheritdoc />
        public Task AddAsync(User user)
        {
            return this.Session.SaveAsync(user);
        }
    }
}
