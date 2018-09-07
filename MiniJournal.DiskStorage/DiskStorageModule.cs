using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace MiniJournal.DiskStorage
{
    public class DiskStorageModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(context => new ImagesService(context.ResolveNamed<string>("ImagesStoragePath")))
                .AsImplementedInterfaces();
        }
    }
}
