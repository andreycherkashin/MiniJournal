using System;
using Infotecs.MiniJournal.WinService.RabbitMq;
using Topshelf;

namespace Infotecs.MiniJournal.WinService
{
    public class WindowsService : ServiceControl
    {
        private readonly RabbitMqListener rabbitMqListener;

        public WindowsService(RabbitMqListener rabbitMqListener)
        {
            this.rabbitMqListener = rabbitMqListener;
        }

        public bool Start(HostControl hostControl)
        {
            this.rabbitMqListener.Start();

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            this.rabbitMqListener.Stop();

            return true;
        }
    }
}
