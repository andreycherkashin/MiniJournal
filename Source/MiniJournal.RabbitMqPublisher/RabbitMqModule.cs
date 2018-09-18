using System;
using Autofac;

namespace Infotecs.MiniJournal.RabbitMqPublisher
{
    /// <inheritdoc />
    public class RabbitMqModule : Module
    {
        private readonly string rabbitMqConnectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMqModule"/> class.
        /// </summary>
        /// <param name="rabbitMqConnectionString">Строка подключения к RabbitMq.</param>
        public RabbitMqModule(string rabbitMqConnectionString)
        {
            this.rabbitMqConnectionString = rabbitMqConnectionString;
        }

        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new RabbitMessageBus(this.rabbitMqConnectionString)).AsImplementedInterfaces().SingleInstance();
            
            builder.RegisterType<EventPublisher>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<CommandDispatcher>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<RabbitMqListener>().AsImplementedInterfaces().SingleInstance();
        }
    }
}
