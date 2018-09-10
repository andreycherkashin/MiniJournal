using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Application;

namespace Infotecs.MiniJournal.PostgreSql
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DbConnectionFactory connectionFactory;

        public UnitOfWork(DbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public Task SaveChangesAsync()
        {
            return this.connectionFactory.CommitTransactionAsync();
        }
    }
}
