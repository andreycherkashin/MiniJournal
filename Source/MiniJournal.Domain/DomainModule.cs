using System;
using Autofac;

namespace Infotecs.MiniJournal.Domain
{
    /// <inheritdoc />
    public class DomainModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(this.ThisAssembly)
                .Where(type => type.Name.EndsWith("Service")
                    || type.Name.EndsWith("Factory"))
                .AsImplementedInterfaces();
        }
    }
}
