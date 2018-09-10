using System;
using Autofac;
using Dapper.FluentMap;
using Infotecs.MiniJournal.PostgreSql.DapperMappings;

namespace Infotecs.MiniJournal.PostgreSql
{
    public class PostgreSqlModule : Autofac.Module
    {
        public static void InitializeMappings()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new UserMap());
                config.AddMap(new CommentMap());
                config.AddMap(new ArticleMap());
            });
        }

        protected override void Load(ContainerBuilder builder)
        {
            InitializeMappings();

            builder
                .RegisterAssemblyTypes(this.ThisAssembly)
                .Where(type => type.Name.EndsWith("Repository") || type.Name.EndsWith("Provider"))
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
