using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.ImagesApplicationsService
{
    /// <summary>
    /// Запрос поиска картинки.
    /// </summary>
    public class FindImageRequest
    {
        /// <summary>
        /// Идентификатор картинки.
        /// </summary>
        public string ImageId { get; set; }
    }
}
