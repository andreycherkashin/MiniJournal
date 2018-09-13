using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infotecs.MiniJournal.Events.Events
{
    /// <summary>
    /// Обработчик события.
    /// </summary>
    /// <typeparam name="TEvent">Тип события.</typeparam>
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        /// <summary>
        /// Обрабатывает событие.
        /// </summary>
        /// <param name="event">Событие.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task HandleAsync(TEvent @event);
    }
}
