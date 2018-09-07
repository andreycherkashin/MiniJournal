using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Autofac;
using AutofacSerilogIntegration;
using Infotecs.MiniJournal.Domain;
using MiniJournal.Application;
using MiniJournal.DiskStorage;
using MiniJournal.PostgreSql;
using Serilog;

namespace Infotecs.MiniJournal.WcfService
{
    public class WcfServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            this.RegisterLogger(builder);    
            this.RegisterTypesAndModules(builder);
            this.RegisterSettings(builder);
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
            builder.RegisterType<AutoMapperConfiguration>().AsSelf().SingleInstance();
            builder.Register(context => context.Resolve<AutoMapperConfiguration>().GetMapper());

            builder.RegisterModule<ApplicationModule>();
            builder.RegisterModule<DomainModule>();
            builder.RegisterModule<PostgreSqlModule>();
            builder.RegisterModule<DiskStorageModule>();
        }
    }
}