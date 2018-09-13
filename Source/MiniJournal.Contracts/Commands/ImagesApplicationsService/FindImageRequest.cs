using System;

namespace Infotecs.MiniJournal.Contracts.Commands.ImagesApplicationsService
{
    /// <summary>
    /// Запрос поиска картинки.
    /// </summary>
    public class FindImageRequest
    {
        /// <summary>
        /// Gets or sets идентификатор картинки.
        /// </summary>
        public string ImageId { get; set; }
    }
}
