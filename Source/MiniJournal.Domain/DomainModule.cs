using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Infotecs.MiniJournal.Domain
{
    public class DomainModule : Autofac.Module
    {
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
