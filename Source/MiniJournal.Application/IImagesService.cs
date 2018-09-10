using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Contracts.ImagesApplicationsService;

namespace Infotecs.MiniJournal.Application
{
    /// <summary>
    /// Сохраняет, возвращает картинки.
    /// </summary>
    public interface IImagesService
    {
        /// <summary>
        /// Находит картинку по идентификатору.
        /// </summary>        
        Task<FindImageResponse> FindImageAsync(FindImageRequest request);

        /// <summary>
        /// Загружает картинку в хранилище.
        /// </summary>
        /// <param name="request">Запрос загрузки картинки.</param>
        /// <returns>Результат запроса картинки.</returns>
        Task<UploadImageResponse> UploadImageAsync(UploadImageRequest request);
    }
}
