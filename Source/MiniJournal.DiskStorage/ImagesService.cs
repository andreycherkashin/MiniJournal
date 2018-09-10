using System;
using System.IO;
using System.Threading.Tasks;
using MiniJournal.Application;

namespace MiniJournal.DiskStorage
{
    public class ImagesService : IImagesService
    {
        private readonly string imagesStoragePath;

        public ImagesService(string imagesStoragePath)
        {
            this.imagesStoragePath = imagesStoragePath;
        }

        public Task<byte[]> FindImageAsync(string imageId)
        {
            return Task.Run(() => File.ReadAllBytes(imageId));
        }

        public async Task<string> UploadImageAsync(byte[] image)
        {
            var guid = Guid.NewGuid().ToString("N");
            var fullImagePath = Path.Combine(this.imagesStoragePath, guid);

            using (var fileStream = new FileStream(fullImagePath, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                await fileStream.WriteAsync(image, 0, image.Length);
            }

            return fullImagePath;
        }
    }
}
