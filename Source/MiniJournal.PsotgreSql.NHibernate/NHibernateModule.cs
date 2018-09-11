using System;
using Autofac;
using FluentNHibernate.Cfg;
using Infotecs.MiniJournal.PostgreSql.NHibernate.Mappings;
using NHibernate;

namespace Infotecs.MiniJournal.PostgreSql.NHibernate
{
    public class NHibernateModule : Autofac.Module
    {
        /// <summary>Override to add registrations to the container.</summary>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
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
                    FluentNHibernate.Cfg.Db.PostgreSQLConfiguration.PostgreSQL81.ConnectionString(context.ResolveNamed<string>("ConnectionString"))
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ArticleMap>())
                .BuildSessionFactory();
        }
    }
}
