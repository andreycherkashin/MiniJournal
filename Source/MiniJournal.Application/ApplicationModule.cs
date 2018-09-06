using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace MiniJournal.Application
{
    /// <summary>
    /// Регистрация компонентов модуля в контейнере зависимостей.
    /// </summary>
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(this.ThisAssembly)
                .Where(type => type.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
        }
    }
}
