using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Npgsql;

namespace MiniJournal.PostgreSql
{
    internal class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string connectionString;

        public DbConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection Create()
        {
            return new NpgsqlConnection(this.connectionString);
        }
    }
}
