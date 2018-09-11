using System;
using NHibernate;

namespace Infotecs.MiniJournal.PostgreSql.NHibernate
{
    public interface ISessionProvider : IDisposable
    {
        ISession GetSession();
    }
}
