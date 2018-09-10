using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.Wcf;
using AutofacSerilogIntegration;
using Infotecs.MiniJournal.Application;
using Infotecs.MiniJournal.DiskStorage;
using Infotecs.MiniJournal.Domain;
using Infotecs.MiniJournal.PostgreSql;
using Serilog;

namespace Infotecs.MiniJournal.WcfService
{
    public class WcfServiceModule : Autofac.Module
    {
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

            builder.RegisterType<ErrorHandling.ErrorHandler>().AsImplementedInterfaces();
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