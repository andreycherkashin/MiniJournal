using System;
using System.IO;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Application;
using Infotecs.MiniJournal.Contracts.ImagesApplicationsService;

namespace Infotecs.MiniJournal.DiskStorage
{
    public class ImagesService : IImagesService
    {
        private readonly string imagesStoragePath;

        public ImagesService(string imagesStoragePath)
        {
            this.imagesStoragePath = imagesStoragePath;
        }

        /// <summary>
        /// Находит картинку по идентификатору.
        /// </summary>        
        public Task<FindImageResponse> FindImageAsync(FindImageRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            byte[] image = null;

            if (File.Exists(request.ImageId))
            {
                image = File.ReadAllBytes(request.ImageId);
            }

            return Task.FromResult(new FindImageResponse(image));
        }

        /// <summary>
        /// Загружает картинку в хранилище.
        /// </summary>
        /// <param name="request">Запрос загрузки картинки.</param>
        /// <returns>Результат запроса картинки.</returns>
        public async Task<UploadImageResponse> UploadImageAsync(UploadImageRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Image == null)
            {
                return new UploadImageResponse();
            }

            var guid = Guid.NewGuid().ToString("N");
            var fullImagePath = Path.Combine(this.imagesStoragePath, guid);

            using (var fileStream = new FileStream(fullImagePath, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                await fileStream.WriteAsync(request.Image, 0, request.Image.Length);
            }

            return new UploadImageResponse(fullImagePath);
        }
    }
}
