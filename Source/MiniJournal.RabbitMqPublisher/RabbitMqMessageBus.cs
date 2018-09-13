using Infotecs.MiniJournal.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Events.Commands;
using Infotecs.MiniJournal.Events.Events;
using RawRabbit;

namespace Infotecs.MiniJournal.RabbitMqPublisher
{
    /// <inheritdoc cref="IMessageBus"/>
    public class RabbitMqMessageBus : IMessageBus, IDisposable
    {
        private readonly IBusClient busClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMqMessageBus"/> class.
        /// </summary>
        /// <param name="busClient"><see cref="IBusClient"/>.</param>
        public RabbitMqMessageBus(IBusClient busClient)
        {
            this.busClient = busClient;
        }

        /// <inheritdoc />
        public void SubscribeToCommand<TCommand>(Func<TCommand, Task> commandHandler)
            where TCommand : ICommand
        {
            this.busClient.SubscribeAsync<TCommand>((message, context) => commandHandler(message));
        }

        /// <inheritdoc />
        public void SubscribeToEvent<TEvent>(Func<TEvent, Task> eventHandler)
            where TEvent : IEvent
        {
            this.busClient.SubscribeAsync<TEvent>((message, context) => eventHandler(message));
        }

        /// <inheritdoc />
        public void SubscribeToCommand<TCommand>(ICommandHandler<TCommand> commandHandler)
            where TCommand : ICommand
        {
            this.busClient.SubscribeAsync<TCommand>((message, context) => commandHandler.HandleAsync(message));
        }

        /// <inheritdoc />
        public void SubscribeToEvent<TEvent>(IEventHandler<TEvent> eventHandler)
            where TEvent : IEvent
        {
            this.busClient.SubscribeAsync<TEvent>((message, context) => eventHandler.HandleAsync(message));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.busClient.ShutdownAsync().Wait();
        }
    }
}
