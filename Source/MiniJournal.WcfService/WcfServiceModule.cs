using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Infotecs.MiniJournal.Domain;
using MiniJournal.Application;
using MiniJournal.DiskStorage;
using MiniJournal.PostgreSql;

namespace Infotecs.MiniJournal.WcfService
{
    public class WcfServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
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