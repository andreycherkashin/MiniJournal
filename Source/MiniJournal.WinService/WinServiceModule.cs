using System;
using System.Configuration;
using Autofac;
using AutofacSerilogIntegration;
using Infotecs.MiniJournal.Application;
using Infotecs.MiniJournal.DiskStorage;
using Infotecs.MiniJournal.Domain;
using Infotecs.MiniJournal.PsotgreSql.NHibernate;
using Infotecs.MiniJournal.WinService.RabbitMq;
using RawRabbit.Common;
using RawRabbit.Configuration;
using RawRabbit.DependencyInjection.Autofac;
using RawRabbit.Logging;
using Serilog;

namespace Infotecs.MiniJournal.WinService
{
    public class WinServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            this.RegisterWinServiceComponents(builder);
            this.RegisterLogger(builder);
            this.RegisterTypesAndModules(builder);
            this.RegisterSettings(builder);
            this.RegisterRabbitMq(builder);
        }

        private void RegisterWinServiceComponents(ContainerBuilder builder)
        {
            builder.RegisterType<WindowsService>().AsSelf().SingleInstance();
            builder.RegisterType<RabbitMqListener>().AsSelf().SingleInstance();
        }

        private void RegisterSettings(ContainerBuilder builder)
        {
            builder.Register(context => ConfigurationManager.AppSettings["ConnectionString"]).Named<string>("ConnectionString");
            builder.Register(context => ConfigurationManager.AppSettings["ImagesStoragePath"]).Named<string>("ImagesStoragePath");
        }

        private void RegisterRabbitMq(ContainerBuilder builder)
        {
            builder.RegisterRawRabbit(ConfigurationManager.AppSettings["RabbitMq"]);
            builder.RegisterType<RawRabbit.Logging.Serilog.LoggerFactory>().As<ILoggerFactory>().SingleInstance();
        }

        private void RegisterLogger(ContainerBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();

            builder.RegisterLogger();
        }

        private void RegisterTypesAndModules(ContainerBuilder builder)
        {
            builder.RegisterModule<ApplicationModule>();
            builder.RegisterModule<DomainModule>();
            builder.RegisterModule<NHibernateModule>();
            builder.RegisterModule<DiskStorageModule>();
        }
    }
}
