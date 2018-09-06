using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniJournal.Application
{
    /// <summary>
    /// Единица бизнес-транзакции.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Применяет изменения сделанные в рамках текущей бизнес транзакции.
        /// </summary>
        Task SaveChangesAsync();
    }
}
