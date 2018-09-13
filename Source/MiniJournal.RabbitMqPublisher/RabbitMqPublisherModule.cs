using System;
using Autofac;
using RawRabbit.Common;
using RawRabbit.Configuration;
using RawRabbit.DependencyInjection.Autofac;
using RawRabbit.Logging;

namespace Infotecs.MiniJournal.RabbitMqPublisher
{
    /// <inheritdoc />
    public class RabbitMqPublisherModule : Module
    {
        private readonly string rabbitMqConnectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMqPublisherModule"/> class.
        /// </summary>
        /// <param name="rabbitMqConnectionString">Строка подключения к RabbitMq.</param>
        public RabbitMqPublisherModule(string rabbitMqConnectionString)
        {
            this.rabbitMqConnectionString = rabbitMqConnectionString;
        }

        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            RawRabbitConfiguration configuration = ConnectionStringParser.Parse(this.rabbitMqConnectionString);

            // configuration.PublishConfirmTimeout = TimeSpan.FromHours(1);
            // configuration.RequestTimeout = TimeSpan.FromHours(1);

            builder.RegisterRawRabbit(configuration);

            builder.RegisterType<RawRabbit.Logging.Serilog.LoggerFactory>().As<ILoggerFactory>().SingleInstance();
            builder.RegisterType<EventPublisher>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<CommandDispatcher>().AsImplementedInterfaces().SingleInstance();
        }
    }
}
