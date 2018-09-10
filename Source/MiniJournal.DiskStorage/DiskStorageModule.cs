using System;
using Autofac;

namespace Infotecs.MiniJournal.DiskStorage
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
