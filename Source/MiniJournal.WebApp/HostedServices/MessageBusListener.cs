using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Events;
using Infotecs.MiniJournal.Events.Events;
using Infotecs.MiniJournal.WebApp.Hubs;
using Microsoft.Extensions.Hosting;

namespace Infotecs.MiniJournal.WebApp.HostedServices
{
    /// <summary>
    /// Отправляет уведомления клиентам.
    /// </summary>
    public class MessageBusListener : IHostedService
    {
        private readonly IMessageBus messageBus;
        private readonly NotificationsHub notificationsHub;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBusListener"/> class.
        /// </summary>
        /// <param name="messageBus"><see cref="IMessageBus"/>.</param>
        /// <param name="notificationsHub"><see cref="NotificationsHub"/>.</param>
        public MessageBusListener(IMessageBus messageBus, NotificationsHub notificationsHub)
        {
            this.messageBus = messageBus;
            this.notificationsHub = notificationsHub;
        }

        /// <inheritdoc />
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.messageBus.SubscribeToEventForNotifications<ArticleCreatedEvent>(this.SendNotification);
            this.messageBus.SubscribeToEventForNotifications<ArticleDeletedEvent>(this.SendNotification);
            this.messageBus.SubscribeToEventForNotifications<CommentAddedEvent>(this.SendNotification);
            this.messageBus.SubscribeToEventForNotifications<CommentDeletedEvent>(this.SendNotification);

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async Task SendNotification<TEvent>(TEvent @event) 
            where TEvent : IEvent
        {
            await this.notificationsHub.Notify<TEvent>(@event);
        }
    }
}
