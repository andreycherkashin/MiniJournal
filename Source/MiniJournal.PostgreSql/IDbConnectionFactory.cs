using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MiniJournal.PostgreSql
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
