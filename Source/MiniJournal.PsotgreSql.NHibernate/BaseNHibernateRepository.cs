using System;
using NHibernate;

namespace Infotecs.MiniJournal.PostgreSql.NHibernate
{
    internal abstract class BaseNHibernateRepository
    {
        private readonly ISessionProvider sessionProvider;

        protected BaseNHibernateRepository(ISessionProvider sessionProvider)
        {
            this.sessionProvider = sessionProvider;
        }

        protected ISession Session => this.sessionProvider.GetSession();
    }
}
