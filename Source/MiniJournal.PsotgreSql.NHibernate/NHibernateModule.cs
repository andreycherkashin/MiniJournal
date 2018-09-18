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
        private readonly string postgresConnectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="NHibernateModule"/> class.
        /// </summary>
        /// <param name="postgresConnectionString">Строка соединения для Postgres.</param>
        public NHibernateModule(string postgresConnectionString)
        {
            this.postgresConnectionString = postgresConnectionString;
        }

        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(this.ThisAssembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder
                .Register(context => new SessionProvider(this.CreateSessionFactory()))
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<UnitOfWork>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        private ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                    PostgreSQLConfiguration.PostgreSQL81.ConnectionString(this.postgresConnectionString)
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ArticleMap>())
                .BuildSessionFactory();
        }
    }
}
