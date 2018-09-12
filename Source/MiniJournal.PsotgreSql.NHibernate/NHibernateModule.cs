using System;
using Autofac;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infotecs.MiniJournal.PostgreSql.NHibernate.Mappings;
using NHibernate;

namespace Infotecs.MiniJournal.PostgreSql.NHibernate
{
    /// <inheritdoc />
    public class NHibernateModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(this.ThisAssembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder
                .Register(context => new SessionProvider(CreateSessionFactory(context)))
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<UnitOfWork>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        private static ISessionFactory CreateSessionFactory(IComponentContext context)
        {
            return Fluently.Configure()
                .Database(
                    PostgreSQLConfiguration.PostgreSQL81.ConnectionString(context.ResolveNamed<string>("ConnectionString"))
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ArticleMap>())
                .BuildSessionFactory();
        }
    }
}
