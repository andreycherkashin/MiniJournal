using System;
using System.IO;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Application;
using Infotecs.MiniJournal.Contracts.ImagesApplicationsService;
using Nelibur.Sword.Extensions;

namespace Infotecs.MiniJournal.DiskStorage
{
    /// <inheritdoc />
    public class ImagesService : IImagesService
    {
        private readonly string imagesStoragePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagesService"/> class.
        /// </summary>
        /// <param name="imagesStoragePath">Путь сохранения картинок.</param>
        public ImagesService(string imagesStoragePath)
        {
            this.imagesStoragePath = imagesStoragePath;
        }

        /// <inheritdoc />
        public Task<FindImageResponse> FindImageAsync(FindImageRequest request)
        {
            return request
                .ToOption()
                .Map(req => Path.Combine(this.imagesStoragePath, req.ImageId))
                .Where(File.Exists)
                .Map(File.ReadAllBytes)
                .Map(FindImageResponse.Create)
                .Map(Task.FromResult)
                .Value;            
        }

        /// <inheritdoc />
        public async Task<UploadImageResponse> UploadImageAsync(UploadImageRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Image == null)
            {
                return new UploadImageResponse();
            }

            string guid = Guid.NewGuid().ToString("N");
            string fullImagePath = Path.Combine(this.imagesStoragePath, guid);

            Directory.CreateDirectory(this.imagesStoragePath);

            using (var fileStream = new FileStream(fullImagePath, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                await fileStream.WriteAsync(request.Image, 0, request.Image.Length);
            }

            return new UploadImageResponse(fullImagePath);
        }
    }
}
