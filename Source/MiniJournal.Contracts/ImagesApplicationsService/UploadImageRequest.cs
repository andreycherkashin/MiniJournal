using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.ImagesApplicationsService
{
    /// <summary>
    /// Запрос загрузки картинки.
    /// </summary>
    public class UploadImageRequest
    {
        /// <summary>
        /// Картинка.
        /// </summary>
        public byte[] Image { get; set; }
    }
}
