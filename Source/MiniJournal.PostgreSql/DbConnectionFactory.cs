using System;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace Infotecs.MiniJournal.PostgreSql
{
    /// <inheritdoc />
    /// <summary>
    /// Фабрика, предоставляющая подключения к базе данных.
    /// </summary>
    internal class DbConnectionFactory : IDbConnectionFactory, IDisposable
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
        internal async Task CommitTransactionAsync()
        {
            await this.transaction?.CommitAsync();
            this.transaction = null;
            
            this.CloseConnection();
        }

        private void CreateConnection()
        {
            this.connection = new NpgsqlConnection(this.connectionString);
            //this.connection = new StackExchange.Profiling.Data.ProfiledDbConnection(new NpgsqlConnection(this.connectionString), StackExchange.Profiling.MiniProfiler.Current);
            this.connection.Open();

            this.transaction?.Dispose();
            this.transaction = this.connection.BeginTransaction();
        }

        private void CloseConnection()
        {
            this.transaction?.Rollback();
            this.connection?.Close();

            this.transaction = null;
            this.connection = null;
        }        

        public void Dispose()
        {            
            this.CloseConnection();
        }
    }
}
