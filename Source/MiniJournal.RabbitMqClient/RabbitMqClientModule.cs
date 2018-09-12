using System;
using Autofac;
using RawRabbit.Common;
using RawRabbit.Configuration;
using RawRabbit.DependencyInjection.Autofac;
using RawRabbit.Logging;
using LoggerFactory = RawRabbit.Logging.Serilog.LoggerFactory;

namespace Infotecs.MiniJournal.RabbitMqClient
{
    /// <inheritdoc />
    public class RabbitMqClientModule : Module
    {
        private readonly string rabbitMqConnectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMqClientModule"/> class.
        /// </summary>
        /// <param name="rabbitMqConnectionString">Строка подключения к RabbitMq.</param>
        public RabbitMqClientModule(string rabbitMqConnectionString)
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

            builder.RegisterType<LoggerFactory>().As<ILoggerFactory>().SingleInstance();
            builder.RegisterType<ArticlesServiceRabbitMqClient>().AsImplementedInterfaces();
        }
    }
}
