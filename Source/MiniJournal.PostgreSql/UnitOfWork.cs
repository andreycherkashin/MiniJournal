using MiniJournal.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniJournal.PostgreSql
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
