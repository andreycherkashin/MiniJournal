﻿using System;
using System.Configuration;
using Autofac;
using AutofacSerilogIntegration;
using Infotecs.MiniJournal.Application;
using Infotecs.MiniJournal.DiskStorage;
using Infotecs.MiniJournal.Domain;
using Infotecs.MiniJournal.PostgreSql.NHibernate;
using Infotecs.MiniJournal.RabbitMqPublisher;
using Infotecs.MiniJournal.WcfService.ErrorHandling;
using Serilog;

namespace Infotecs.MiniJournal.WcfService
{
    /// <inheritdoc />
    public class WcfServiceModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            this.RegisterWcfComponents(builder);
            this.RegisterLogger(builder);
            this.RegisterTypesAndModules(builder);
            this.RegisterSettings(builder);
        }

        private void RegisterWcfComponents(ContainerBuilder builder)
        {
            builder.RegisterType<ArticlesWebService>().AsSelf().AsImplementedInterfaces();

            builder.RegisterType<ErrorHandler>().AsImplementedInterfaces();
        }

        private void RegisterSettings(ContainerBuilder builder)
        {
            builder.Register(context => ConfigurationManager.AppSettings["ConnectionString"]).Named<string>("ConnectionString");
            builder.Register(context => ConfigurationManager.AppSettings["ImagesStoragePath"]).Named<string>("ImagesStoragePath");
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
            builder.RegisterModule(new RabbitMqModule(ConfigurationManager.AppSettings["RabbitMq"]));
        }
    }
}
