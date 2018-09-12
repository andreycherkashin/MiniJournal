using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using RawRabbit.Common;
using RawRabbit.DependencyInjection.Autofac;
using RawRabbit.Logging;

namespace Infotecs.MiniJournal.RabbitMqClient
{
    public class RabbitMqClientModule : Autofac.Module
    {
        private readonly string rabbitMqConnectionString;

        public RabbitMqClientModule(string rabbitMqConnectionString)
        {
            this.rabbitMqConnectionString = rabbitMqConnectionString;
        }

        /// <summary>Override to add registrations to the container.</summary>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
            var configuration = ConnectionStringParser.Parse(this.rabbitMqConnectionString);

            // configuration.PublishConfirmTimeout = TimeSpan.FromHours(1);
            // configuration.RequestTimeout = TimeSpan.FromHours(1);

            builder.RegisterRawRabbit(configuration);

            builder.RegisterType<RawRabbit.Logging.Serilog.LoggerFactory>().As<ILoggerFactory>().SingleInstance();
            builder.RegisterType<ArticlesServiceRabbitMqClient>().AsImplementedInterfaces();
        }
    }
}
