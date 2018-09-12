using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Application;

namespace Infotecs.MiniJournal.PostgreSql.NHibernate
{
    /// <inheritdoc />
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly SessionProvider sessionProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="sessionProvider"><see cref="ISessionProvider"/>.</param>
        public UnitOfWork(SessionProvider sessionProvider)
        {
            this.sessionProvider = sessionProvider;
        }

        /// <inheritdoc />
        public Task SaveChangesAsync()
        {
            return this.sessionProvider.SaveChangesAsync();
        }
    }
}
