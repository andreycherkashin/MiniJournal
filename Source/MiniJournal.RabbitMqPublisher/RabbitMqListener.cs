using Infotecs.MiniJournal.Events;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Events.Commands;
using Infotecs.MiniJournal.Events.Events;
using RawRabbit;
using RawRabbit.Configuration.Exchange;

namespace Infotecs.MiniJournal.RabbitMqPublisher
{
    /// <inheritdoc cref="IMessageBus"/>
    public class RabbitMqListener : IMessageBus, IDisposable
    {
        private static readonly string uniqueClientId = Guid.NewGuid().ToString("N");

        private readonly IBusClient busClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMqListener"/> class.
        /// </summary>
        /// <param name="busClient"><see cref="IBusClient"/>.</param>
        public RabbitMqListener(IBusClient busClient)
        {
            this.busClient = busClient;
        }

        /// <inheritdoc />
        public void SubscribeToCommand<TCommand>(Func<TCommand, Task> commandHandler)
            where TCommand : ICommand
        {
            this.busClient.SubscribeAsync<TCommand>((message, context) => commandHandler(message));

            //this.busClient.SubscribeAsync<TCommand>((message, context) => commandHandler(message),
            //    configuration: configuration =>
            //    {
            //        configuration
            //            .WithQueue(queue =>
            //            {
            //                queue
            //                    .WithName(RabbitMqConfiguration.GetQueueNameForCommand<TCommand>())
            //                    .WithNameSuffix(string.Empty);
            //            })
            //            .WithExchange(exchange => exchange.WithName(RabbitMqConfiguration.GetExchangeNameForCommand<TCommand>()))
            //            .WithRoutingKey(RabbitMqConfiguration.GetBindingKeyForCommand<TCommand>());
            //    });
        }

        /// <inheritdoc />
        public void SubscribeToEvent<TEvent>(Func<TEvent, Task> eventHandler)
            where TEvent : IEvent
        {
            this.SubscribeToEvent(eventHandler, forNotifications: false);
        }

        /// <inheritdoc />
        public void SubscribeToEventForNotifications<TEvent>(Func<TEvent, Task> eventHandler)
            where TEvent : IEvent
        {
            this.SubscribeToEvent(eventHandler, forNotifications: true);
        }

        /// <inheritdoc />
        public void SubscribeToCommand<TCommand>(ICommandHandler<TCommand> commandHandler)
            where TCommand : ICommand
        {
            this.SubscribeToCommand<TCommand>(commandHandler.HandleAsync);
        }

        /// <inheritdoc />
        public void SubscribeToEvent<TEvent>(IEventHandler<TEvent> eventHandler)
            where TEvent : IEvent
        {
            this.SubscribeToEvent<TEvent>(eventHandler.HandleAsync, forNotifications: false);
        }

        /// <inheritdoc />
        public void SubscribeToEventForNotifications<TEvent>(IEventHandler<TEvent> eventHandler)
            where TEvent : IEvent
        {
            this.SubscribeToEvent<TEvent>(eventHandler.HandleAsync, forNotifications: true);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.busClient.ShutdownAsync();
        }

        private void SubscribeToEvent<TEvent>(Func<TEvent, Task> eventHandler, bool forNotifications)
            where TEvent : IEvent
        {
            this.busClient.SubscribeAsync<TEvent>((message, context) => eventHandler(message), cfg =>
            {
                if (forNotifications)
                {
                    cfg
                        .WithSubscriberId($"{Assembly.GetEntryAssembly().GetName().Name}.{uniqueClientId}")
                        .WithQueue(queue => queue.WithAutoDelete());
                }
            });
        }
    }
}
