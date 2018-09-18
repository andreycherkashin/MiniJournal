using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Events;
using Infotecs.MiniJournal.Events.Events;

namespace Infotecs.MiniJournal.RabbitMqPublisher
{
    /// <inheritdoc />
    public class EventPublisher : IEventPublisher
    {
        private readonly IRabbitMessageBus busClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventPublisher"/> class.
        /// </summary>
        /// <param name="busClient"><see cref="IRabbitMessageBus"/>.</param>
        public EventPublisher(IRabbitMessageBus busClient)
        {
            this.busClient = busClient;
        }

        /// <inheritdoc />
        public async Task PublishAsync<T>(T @event) where T : IEvent
        {
            await this.busClient.PublishAsync(@event);
        }
    }
}
