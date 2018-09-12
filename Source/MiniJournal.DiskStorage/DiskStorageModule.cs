using System;
using Autofac;

namespace Infotecs.MiniJournal.DiskStorage
{
    /// <inheritdoc />
    public class DiskStorageModule : Module
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(context => new ImagesService(context.ResolveNamed<string>("ImagesStoragePath")))
                .AsImplementedInterfaces();
        }
    }
}
