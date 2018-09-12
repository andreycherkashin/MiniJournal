using System;
using NHibernate;

namespace Infotecs.MiniJournal.PostgreSql.NHibernate
{
    /// <summary>
    /// Провайдер сессий для репозиториев.
    /// </summary>
    public interface ISessionProvider : IDisposable
    {
        /// <summary>
        /// Возвращает текущую сессию.
        /// </summary>
        /// <returns>Текущая сессия.</returns>
        ISession GetSession();
    }
}
