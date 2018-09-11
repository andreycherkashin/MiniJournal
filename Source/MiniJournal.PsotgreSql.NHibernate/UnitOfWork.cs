using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Application;

namespace Infotecs.MiniJournal.PostgreSql.NHibernate
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly SessionProvider sessionProvider;

        public UnitOfWork(SessionProvider sessionProvider)
        {
            this.sessionProvider = sessionProvider;
        }

        /// <summary>
        /// Применяет изменения сделанные в рамках текущей бизнес транзакции.
        /// </summary>
        public Task SaveChangesAsync()
            => this.sessionProvider.SaveChangesAsync();
    }
}
