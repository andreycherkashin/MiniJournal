using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Contracts.Events;

namespace Infotecs.MiniJournal.Contracts
{
    /// <summary>
    /// Класс публикации событий.
    /// </summary>
    public interface IEventPublisher
    {
        /// <summary>
        /// Публикует событие.
        /// </summary>
        /// <typeparam name="T">Тип события.</typeparam>
        /// <param name="event">Событие.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task PublishAsync<T>(T @event)
            where T : IEvent;
    }
}
