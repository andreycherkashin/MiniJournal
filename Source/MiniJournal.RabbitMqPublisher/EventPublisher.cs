using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Application;
using Infotecs.MiniJournal.Contracts;
using Infotecs.MiniJournal.Contracts.Events;
using RawRabbit;

namespace Infotecs.MiniJournal.RabbitMqPublisher
{
    /// <inheritdoc />
    public class EventPublisher : IEventPublisher
    {
        private readonly IBusClient busClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventPublisher"/> class.
        /// </summary>
        /// <param name="busClient"><see cref="IBusClient"/>.</param>
        public EventPublisher(IBusClient busClient)
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
