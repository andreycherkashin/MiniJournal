using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Infotecs.MiniJournal.Contracts.Events;
using RawRabbit;

namespace Infotecs.MiniJournal.WpfClient
{
    /// <inheritdoc cref="IMessageBusListener"/>
    public class MessageBusListener : IMessageBusListener, IDisposable
    {
        private readonly ConcurrentDictionary<Type, List<Subscriber>> subscribers = new ConcurrentDictionary<Type, List<Subscriber>>();
        private readonly IBusClient busClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBusListener"/> class.
        /// </summary>
        /// <param name="busClient"><see cref="IBusClient"/>.</param>
        public MessageBusListener(IBusClient busClient)
        {
            busClient.SubscribeAsync<UserCreatedEvent>((@event, context) => this.OnEventReceived(@event));
            busClient.SubscribeAsync<ArticleCreatedEvent>((@event, context) => this.OnEventReceived(@event));
            busClient.SubscribeAsync<ArticleDeletedEvent>((@event, context) => this.OnEventReceived(@event));
            busClient.SubscribeAsync<CommentAddedEvent>((@event, context) => this.OnEventReceived(@event));
            busClient.SubscribeAsync<CommentDeletedEvent>((@event, context) => this.OnEventReceived(@event));
            this.busClient = busClient;
        }

        /// <inheritdoc />
        public void Subscribe<T>(Func<T, Task> eventHandler) where T : IEvent
        {
            Func<T, bool> eventFilter = _ => true;
            this.AddSubscriber(typeof(T), eventFilter, eventHandler, false);
        }

        /// <inheritdoc />
        public void Subscribe<T>(Func<T, bool> eventFilter, Func<T, Task> eventHandler) where T : IEvent
        {
            this.AddSubscriber(typeof(T), eventFilter, eventHandler, false);
        }

        /// <inheritdoc />
        public void SubscribeOnce<T>(Func<T, bool> eventFilter, Func<T, Task> eventHandler) where T : IEvent
        {
            this.AddSubscriber(typeof(T), eventFilter, eventHandler, true);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.busClient.ShutdownAsync(TimeSpan.FromSeconds(1)).Wait(TimeSpan.FromSeconds(1));
        }

        private async Task OnEventReceived<T>(T @event)
        {
            if (!this.subscribers.TryGetValue(typeof(T), out var eventSubscribers))
            {
                return;
            }

            foreach (var subscriber in eventSubscribers)
            {
                var eventMatcher = (Func<T, bool>)subscriber.EventMatcher;
                var action = (Func<T, Task>)subscriber.Action;

                if (!eventMatcher(@event))
                {
                    continue;
                }

                if (subscriber.Once && subscriber.Triggered)
                {
                    continue;
                }

                try
                {
                    await action(@event);
                    subscriber.Triggered = true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Exception while invoking subscriber on: " + typeof(T).Name, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddSubscriber(Type eventType, Delegate eventMatcher, Delegate action, bool once)
        {
            if (!this.subscribers.TryGetValue(eventType, out var eventSubscribers))
            {
                eventSubscribers = new List<Subscriber>();
                this.subscribers[eventType] = eventSubscribers;
            }

            lock (eventSubscribers)
            {
                eventSubscribers.RemoveAll(x => x.Once && x.Triggered);
                eventSubscribers.Add(new Subscriber(eventMatcher, action, once));
            }
        }

        private class Subscriber
        {
            public Subscriber(Delegate eventMatcher, Delegate action, bool once)
            {
                this.EventMatcher = eventMatcher;
                this.Action = action;
                this.Once = once;
            }

            public Delegate EventMatcher { get; }
            public Delegate Action { get; }
            public bool Once { get; }
            public bool Triggered { get; set; }
        }
    }
}
