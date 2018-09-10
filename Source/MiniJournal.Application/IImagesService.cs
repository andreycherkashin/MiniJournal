using System;
using System.Threading.Tasks;

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
        /// <param name="imageId">Идентификатор картинки.</param>
        /// <returns>Массив байт, который представляет из себя картинку.</returns>
        Task<byte[]> FindImageAsync(string imageId);

        /// <summary>
        /// Загружает картинку в хранилище.
        /// </summary>
        /// <param name="image">Картинка.</param>
        /// <returns>Идентификатор загруженной картинки.</returns>
        Task<string> UploadImageAsync(byte[] image);
    }
}
