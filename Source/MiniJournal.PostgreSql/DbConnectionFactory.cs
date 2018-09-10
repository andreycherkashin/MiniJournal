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
            //if (this.connection == null)
            //{
            //    this.CreateConnection();
            //}

            //return this.connection;

            var connection = new NpgsqlConnection(this.connectionString);
            return new StackExchange.Profiling.Data.ProfiledDbConnection(connection, StackExchange.Profiling.MiniProfiler.Current);
        }

        /// <summary>
        /// Комитит транзакцию.
        /// </summary>
        internal async Task CommitTransactionAsync()
        {
            //await this.transaction?.CommitAsync();
            //this.connection?.Close();
            //this.connection = null;
        }

        private void CreateConnection()
        {
            this.connection = new NpgsqlConnection(this.connectionString);
            this.connection.Open();
            this.transaction = this.connection.BeginTransaction();
        }

        public void Dispose()
        {
            //this.transaction?.Rollback();
            //this.connection?.Dispose();
        }
    }
}
