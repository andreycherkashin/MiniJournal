using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace MiniJournal.PostgreSql
{
    /// <inheritdoc />
    /// <summary>
    /// Фабрика, предоставляющая подключения к базе данных.
    /// </summary>
    internal class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string connectionString;

        private NpgsqlConnection connection;
        private NpgsqlTransaction transaction;

        public DbConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <inheritdoc />
        /// <summary>
        /// Возвращает соединение к базе данных.
        /// </summary>
        /// <returns>Соединение к базе данных</returns>
        public IDbConnection GetConnection()
        {
            if (this.connection == null)
            {
                this.CreateConnection();
            }

            return this.connection;
        }

        /// <summary>
        /// Комитит транзакцию.
        /// </summary>
        internal Task CommitTransactionAsync()
        {
            return this.transaction?.CommitAsync();
        }

        private void CreateConnection()
        {
            this.connection = new NpgsqlConnection(this.connectionString);
            this.transaction = this.connection.BeginTransaction();
        }
    }
}
