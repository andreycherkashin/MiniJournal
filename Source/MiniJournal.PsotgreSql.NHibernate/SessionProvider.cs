using System;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using Infotecs.MiniJournal.PostgreSql.NHibernate.Mappings;
using NHibernate;

namespace Infotecs.MiniJournal.PostgreSql.NHibernate
{
    internal class SessionProvider : ISessionProvider, IDisposable
    {        
        private readonly ISessionFactory sessionFactory;

        private ISession session;
        private ITransaction transaction;

        public SessionProvider(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }        

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

        public async Task SaveChangesAsync()
        {
            await this.transaction?.CommitAsync();
            this.transaction?.Dispose();
            this.transaction = null;

            this.CloseSession();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
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
