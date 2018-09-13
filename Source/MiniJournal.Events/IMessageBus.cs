using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Events.Commands;
using Infotecs.MiniJournal.Events.Events;

namespace Infotecs.MiniJournal.Events
{
    /// <summary>
    /// Абстракция шины сообщений.
    /// </summary>
    public interface IMessageBus : IDisposable
    {
        /// <summary>
        /// Подписывает на команды.
        /// </summary>
        /// <typeparam name="TCommand">Тип команды.</typeparam>
        /// <param name="commandHandler">Обработчик команды.</param>
        void SubscribeToCommand<TCommand>(Func<TCommand, Task> commandHandler) where TCommand : ICommand;

        /// <summary>
        /// Подписывает на события.
        /// </summary>
        /// <typeparam name="TEvent">Тип события.</typeparam>
        /// <param name="eventHandler">Обработчик события.</param>
        void SubscribeToEvent<TEvent>(Func<TEvent, Task> eventHandler);

        /// <summary>
        /// Подписывает на команды.
        /// </summary>
        /// <typeparam name="TCommand">Тип команды.</typeparam>
        /// <param name="commandHandler">Обработчик команды.</param>
        void SubscribeToCommand<TCommand>(ICommandHandler<TCommand> commandHandler) where TCommand : ICommand;

        /// <summary>
        /// Подписывает на события.
        /// </summary>
        /// <typeparam name="TEvent">Тип события.</typeparam>
        /// <param name="eventHandler">Обработчик события.</param>
        void SubscribeToEvent<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : IEvent;
    }
}
