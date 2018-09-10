﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.ImagesApplicationsService
{
    /// <summary>
    /// Результат запроса загрузки картинки.
    /// </summary>
    public class UploadImageResponse
    {
        /// <summary>
        /// Идентификатор загруженной картинки, если загрузка завершилась успешно.
        /// </summary>
        public string ImageId { get; set; }
    }
}
