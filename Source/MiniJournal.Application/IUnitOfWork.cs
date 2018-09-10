using System;
using System.Threading.Tasks;

namespace Infotecs.MiniJournal.Application
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
