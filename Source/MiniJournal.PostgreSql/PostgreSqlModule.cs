using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Dapper.FluentMap;
using MiniJournal.PostgreSql.DapperMappings;

namespace MiniJournal.PostgreSql
{
    public class PostgreSqlModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new UserMap());
                config.AddMap(new CommentMap());
                config.AddMap(new ArticleMap());
            });

            builder
                .RegisterAssemblyTypes(this.ThisAssembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .Register(context => new DbConnectionFactory(context.ResolveNamed<string>("ConnectionString")))
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<UnitOfWork>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
