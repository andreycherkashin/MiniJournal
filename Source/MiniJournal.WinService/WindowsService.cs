using System;
using Infotecs.MiniJournal.WinService.RabbitMq;
using Topshelf;

namespace Infotecs.MiniJournal.WinService
{
    /// <inheritdoc/>
    public class WindowsService : ServiceControl
    {
        private readonly RabbitMqListener rabbitMqListener;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsService"/> class.
        /// </summary>
        /// <param name="rabbitMqListener">Слушатель очереди.</param>
        public WindowsService(RabbitMqListener rabbitMqListener)
        {
            this.rabbitMqListener = rabbitMqListener;
        }

        /// <summary>
        /// Запускает службу.
        /// </summary>
        /// <param name="hostControl">Хост.</param>
        /// <returns>Успешно ли запущена служба.</returns>
        public bool Start(HostControl hostControl)
        {
            this.rabbitMqListener.Start();

            return true;
        }

        /// <summary>
        /// Останавливает службу.
        /// </summary>
        /// <param name="hostControl">Хост.</param>
        /// <returns>Успешно ли остановлена служба.</returns>
        public bool Stop(HostControl hostControl)
        {
            this.rabbitMqListener.Stop();

            return true;
        }
    }
}
