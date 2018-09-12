using System;
using Autofac;

namespace Infotecs.MiniJournal.Application
{
    /// <inheritdoc />
    public class ApplicationModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(this.ThisAssembly)
                .Where(type => type.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            builder.RegisterType<AutoMapperConfiguration>().AsSelf().SingleInstance();
            builder.Register(context => context.Resolve<AutoMapperConfiguration>().GetMapper());
        }
    }
}
