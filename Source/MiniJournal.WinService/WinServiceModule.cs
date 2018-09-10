using System;
using System.Configuration;
using Autofac;
using AutofacSerilogIntegration;
using Infotecs.MiniJournal.Application;
using Infotecs.MiniJournal.DiskStorage;
using Infotecs.MiniJournal.Domain;
using Infotecs.MiniJournal.PostgreSql;
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
        }

        private void RegisterWinServiceComponents(ContainerBuilder builder)
        {
            builder
                .RegisterType<WindowsService>()
                .AsSelf()
                .SingleInstance();
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
            builder.RegisterModule<PostgreSqlModule>();
            builder.RegisterModule<DiskStorageModule>();
        }
    }
}
