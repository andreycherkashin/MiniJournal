using System;
using System.Threading.Tasks;
using NHibernate;

namespace Infotecs.MiniJournal.PostgreSql.NHibernate
{
    /// <inheritdoc cref="IServiceProvider"/>
    internal class SessionProvider : ISessionProvider, IDisposable
    {
        private readonly ISessionFactory sessionFactory;

        private ISession session;
        private ITransaction transaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionProvider"/> class.
        /// </summary>
        /// <param name="sessionFactory">Фабрика сессий.</param>
        public SessionProvider(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        /// <inheritdoc />
        public ISession GetSession()
        {
            if (this.session == null)
            {
                this.session = this.sessionFactory.OpenSession();
            }

            if (this.transaction == null)
            {
                this.transaction = this.session.BeginTransaction();
            }

            return this.session;
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            this.CloseSession();
        }

        /// <summary>
        /// Комитит текущую транзакцию текущей сессии.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task SaveChangesAsync()
        {
            if (this.transaction != null)
            {
                await this.transaction.CommitAsync();
            }

            this.transaction?.Dispose();
            this.transaction = null;

            this.CloseSession();
        }

        private void CloseSession()
        {
            this.transaction?.Rollback();
            this.transaction?.Dispose();
            this.transaction = null;

            this.session?.Dispose();
            this.session = null;
        }
    }
}
