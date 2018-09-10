using System;
using System.Data;

namespace Infotecs.MiniJournal.PostgreSql
{
    /// <summary>
    /// Фабрика, предоставляющая подключения к базе данных.
    /// </summary>
    internal interface IDbConnectionFactory : IDisposable
    {
        /// <summary>
        /// Возвращает соединение к базе данных.
        /// </summary>
        /// <returns>Соединение к базе данных</returns>
        IDbConnection GetConnection();
    }
}
