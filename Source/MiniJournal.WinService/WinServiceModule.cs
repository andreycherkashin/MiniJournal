﻿using System;
using System.Configuration;
using Autofac;
using AutofacSerilogIntegration;
using Infotecs.MiniJournal.Application;
using Infotecs.MiniJournal.DiskStorage;
using Infotecs.MiniJournal.Domain;
using Infotecs.MiniJournal.PostgreSql.NHibernate;
using Infotecs.MiniJournal.RabbitMqPublisher;
using Infotecs.MiniJournal.WinService.RabbitMq;
using RawRabbit.DependencyInjection.Autofac;
using RawRabbit.Logging;
using Serilog;
using LoggerFactory = RawRabbit.Logging.Serilog.LoggerFactory;

namespace Infotecs.MiniJournal.WinService
{
    /// <inheritdoc/>
    public class WinServiceModule : Module
    {
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            RegisterWinServiceComponents(builder);
            RegisterLogger(builder);
            RegisterTypesAndModules(builder);
            RegisterSettings(builder);
            RegisterRabbitMq(builder);
        }

        private static void RegisterWinServiceComponents(ContainerBuilder builder)
        {
            builder.RegisterType<WindowsService>().AsSelf().SingleInstance();
            builder.RegisterType<RabbitMqListener>().AsSelf().SingleInstance();
        }

        private static void RegisterSettings(ContainerBuilder builder)
        {
            builder.Register(context => ConfigurationManager.AppSettings["ConnectionString"]).Named<string>("ConnectionString");
            builder.Register(context => ConfigurationManager.AppSettings["ImagesStoragePath"]).Named<string>("ImagesStoragePath");
        }

        private static void RegisterRabbitMq(ContainerBuilder builder)
        {
            builder.RegisterRawRabbit(ConfigurationManager.AppSettings["RabbitMq"]);
            builder.RegisterType<LoggerFactory>().As<ILoggerFactory>().SingleInstance();
        }

        private static void RegisterLogger(ContainerBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();

            builder.RegisterLogger();
        }

        private static void RegisterTypesAndModules(ContainerBuilder builder)
        {
            builder.RegisterModule<ApplicationModule>();
            builder.RegisterModule<DomainModule>();
            builder.RegisterModule<NHibernateModule>();
            builder.RegisterModule<DiskStorageModule>();
            builder.RegisterModule(new RabbitMqPublisherModule(ConfigurationManager.AppSettings["RabbitMq"]));
        }
    }
}
