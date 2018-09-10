using RawRabbit;
using System;

namespace Infotecs.MiniJournal.WinService.RabbitMq
{
    public class RabbitMqListener
    {
        private readonly IBusClient busClient;

        public RabbitMqListener(IBusClient busClient)
        {
            this.busClient = busClient;
        }

        public void Start()
        {
            //this.busClient.
        }

        public void Stop()
        {

        }
    }
}
