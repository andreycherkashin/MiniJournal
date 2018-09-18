using System;
using Autofac;

namespace Infotecs.MiniJournal.DiskStorage
{
    /// <inheritdoc />
    public class DiskStorageModule : Module
    {
        private readonly string imageStoragePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiskStorageModule"/> class.
        /// </summary>
        /// <param name="imageStoragePath">Путь сохранения картинок.</param>
        public DiskStorageModule(string imageStoragePath)
        {
            this.imageStoragePath = imageStoragePath;
        }

        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(context => new ImagesService(this.imageStoragePath))
                .AsImplementedInterfaces();
        }
    }
}
