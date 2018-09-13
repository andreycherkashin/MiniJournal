using System;
using Topshelf;

namespace Infotecs.MiniJournal.WinService
{
    /// <inheritdoc/>
    public class WindowsService : ServiceControl
    {
        private readonly CommandsDispatcher commandsDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsService"/> class.
        /// </summary>
        /// <param name="commandsDispatcher">Слушатель очереди.</param>
        public WindowsService(CommandsDispatcher commandsDispatcher)
        {
            this.commandsDispatcher = commandsDispatcher;
        }

        /// <summary>
        /// Запускает службу.
        /// </summary>
        /// <param name="hostControl">Хост.</param>
        /// <returns>Успешно ли запущена служба.</returns>
        public bool Start(HostControl hostControl)
        {
            this.commandsDispatcher.Start();

            return true;
        }

        /// <summary>
        /// Останавливает службу.
        /// </summary>
        /// <param name="hostControl">Хост.</param>
        /// <returns>Успешно ли остановлена служба.</returns>
        public bool Stop(HostControl hostControl)
        {
            this.commandsDispatcher.Stop();

            return true;
        }
    }
}
