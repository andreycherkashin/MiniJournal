using System;

namespace Infotecs.MiniJournal.Contracts.ImagesApplicationsService
{
    /// <summary>
    /// Результат запроса загрузки картинки.
    /// </summary>
    public class UploadImageResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadImageResponse"/> class.
        /// </summary>
        public UploadImageResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadImageResponse"/> class.
        /// </summary>
        /// <param name="imageId">Идентификатор картинки.</param>
        public UploadImageResponse(string imageId)
        {
            this.ImageId = imageId;
        }

        /// <summary>
        /// Gets or sets идентификатор загруженной картинки, если загрузка завершилась успешно.
        /// </summary>
        public string ImageId { get; set; }
    }
}
