using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Contracts;
using Infotecs.MiniJournal.Contracts.Commands;
using RawRabbit;

namespace Infotecs.MiniJournal.RabbitMqPublisher
{
    /// <inheritdoc />
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IBusClient busClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDispatcher"/> class.
        /// </summary>
        /// <param name="busClient"><see cref="IBusClient"/>.</param>
        public CommandDispatcher(IBusClient busClient)
        {
            this.busClient = busClient;
        }

        /// <inheritdoc />
        public async Task DispatchAsync<T>(T command)
            where T : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            await this.busClient.PublishAsync(command);
        }
    }
}
