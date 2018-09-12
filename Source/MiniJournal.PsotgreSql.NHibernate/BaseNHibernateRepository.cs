using System;
using NHibernate;

namespace Infotecs.MiniJournal.PostgreSql.NHibernate
{
    /// <summary>
    /// Базовый класс для реализации репозиториев с использованием NHibernate.
    /// </summary>
    internal abstract class BaseNHibernateRepository
    {
        private readonly ISessionProvider sessionProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseNHibernateRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">Провайдер текущей сессии.</param>
        protected BaseNHibernateRepository(ISessionProvider sessionProvider)
        {
            this.sessionProvider = sessionProvider;
        }

        /// <summary>
        /// Текущая сессия.
        /// </summary>
        protected ISession Session
            => this.sessionProvider.GetSession();
    }
}
