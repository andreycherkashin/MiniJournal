using System;

namespace Infotecs.MiniJournal.Contracts.ImagesApplicationsService
{
    /// <summary>
    /// Запрос загрузки картинки.
    /// </summary>
    public class UploadImageRequest
    {
        /// <summary>
        /// Gets or sets картинка.
        /// </summary>
        public byte[] Image { get; set; }
    }
}
