using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Contracts.Events;

namespace Infotecs.MiniJournal.WpfClient
{
    /// <summary>
    /// Позволяет подписаться на события.
    /// </summary>
    public interface IMessageBusListener : IDisposable
    {
        /// <summary>
        /// Подписывает на события, при этом делегат <paramref name="eventHandler"/> будет вызываться всякий раз, как будет получено сообщение.
        /// </summary>
        /// <typeparam name="T">Тип сообщения.</typeparam>
        /// <param name="eventHandler">Обработчик события.</param>
        void Subscribe<T>(Func<T, Task> eventHandler)
            where T : IEvent;

        /// <summary>
        /// Подписывает на события, при этом делегат <paramref name="eventHandler"/> будет вызываться всякий раз, как будет получено сообщение.
        /// </summary>
        /// <typeparam name="T">Тип сообщения.</typeparam>
        /// <param name="eventFilter">Фильтр сообщений.</param>
        /// <param name="eventHandler">Обработчик события.</param>
        void Subscribe<T>(Func<T, bool> eventFilter, Func<T, Task> eventHandler)
            where T : IEvent;

        /// <summary>
        /// Подписывает на события, при этом делегат <paramref name="eventHandler"/> будет вызываться только один раз, как будет получено первое сообщение удовлетворяющее фильтру <paramref name="eventFilter"/>.
        /// </summary>
        /// <typeparam name="T">Тип сообщения.</typeparam>
        /// <param name="eventFilter">Фильтр сообщений.</param>
        /// <param name="eventHandler">Обработчик события.</param>
        void SubscribeOnce<T>(Func<T, bool> eventFilter, Func<T, Task> eventHandler)
            where T : IEvent;
    }
}
