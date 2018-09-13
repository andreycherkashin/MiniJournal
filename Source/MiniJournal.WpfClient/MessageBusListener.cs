using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Infotecs.MiniJournal.Events;
using Infotecs.MiniJournal.Events.Events;

namespace Infotecs.MiniJournal.WpfClient
{
    /// <inheritdoc cref="IMessageBusListener"/>
    public class MessageBusListener : IMessageBusListener, IDisposable
    {
        private readonly ConcurrentDictionary<Type, List<Subscriber>> subscribers = new ConcurrentDictionary<Type, List<Subscriber>>();
        private readonly IMessageBus messageBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBusListener"/> class.
        /// </summary>
        /// <param name="messageBus"><see cref="IMessageBus"/>.</param>
        public MessageBusListener(IMessageBus messageBus)
        {
            messageBus.SubscribeToEvent<UserCreatedEvent>(this.OnEventReceived);
            messageBus.SubscribeToEvent<ArticleCreatedEvent>(this.OnEventReceived);
            messageBus.SubscribeToEvent<ArticleDeletedEvent>(this.OnEventReceived);
            messageBus.SubscribeToEvent<CommentAddedEvent>(this.OnEventReceived);
            messageBus.SubscribeToEvent<CommentDeletedEvent>(this.OnEventReceived);
            this.messageBus = messageBus;
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
            this.messageBus.Dispose();
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
                    await Application.Current.Dispatcher.InvokeAsync(async () => await action(@event));
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
