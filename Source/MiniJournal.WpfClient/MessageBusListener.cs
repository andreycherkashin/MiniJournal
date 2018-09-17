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
            this.messageBus = messageBus;
        }

        /// <inheritdoc />
        public void Subscribe<T>(Func<T, Task> eventHandler) where T : IEvent
        {
            Func<T, bool> eventFilter = _ => true;
            this.AddSubscriber<T>(eventFilter, eventHandler, false);
        }

        /// <inheritdoc />
        public void Subscribe<T>(Func<T, bool> eventFilter, Func<T, Task> eventHandler) where T : IEvent
        {
            this.AddSubscriber<T>(eventFilter, eventHandler, false);
        }

        /// <inheritdoc />
        public void SubscribeOnce<T>(Func<T, bool> eventFilter, Func<T, Task> eventHandler) where T : IEvent
        {
            this.AddSubscriber<T>(eventFilter, eventHandler, true);
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
                var eventFilter = (Func<T, bool>)subscriber.EventFilter;
                var action = (Func<T, Task>)subscriber.Action;

                if (!eventFilter(@event))
                {
                    continue;
                }

                if (subscriber.Once && subscriber.Triggered)
                {
                    continue;
                }

                try
                {
                    await Application.Current.Dispatcher.InvokeAsync(() => action(@event));
                    subscriber.Triggered = true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Exception while invoking subscriber on: " + typeof(T).Name, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddSubscriber<TEvent>(Delegate eventMatcher, Delegate action, bool once)
            where TEvent : IEvent
        {            
            if (!this.subscribers.TryGetValue(typeof(TEvent), out var eventSubscribers))
            {
                eventSubscribers = new List<Subscriber>();
                this.messageBus.SubscribeToEventForNotifications<TEvent>(this.OnEventReceived);
                this.subscribers[typeof(TEvent)] = eventSubscribers;                
            }

            lock (eventSubscribers)
            {
                eventSubscribers.RemoveAll(x => x.Once && x.Triggered);
                eventSubscribers.Add(new Subscriber(eventMatcher, action, once));
            }
        }

        private class Subscriber
        {
            public Subscriber(Delegate eventFilter, Delegate action, bool once)
            {
                this.EventFilter = eventFilter;
                this.Action = action;
                this.Once = once;
            }

            public Delegate EventFilter { get; }
            public Delegate Action { get; }
            public bool Once { get; }
            public bool Triggered { get; set; }
        }
    }
}
