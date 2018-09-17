using System;

namespace Infotecs.MiniJournal.Contracts.ImagesApplicationsService
{
    /// <summary>
    /// Результат запроса поиска картинки.
    /// </summary>
    public class FindImageResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindImageResponse"/> class.
        /// </summary>
        public FindImageResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindImageResponse"/> class.
        /// </summary>
        /// <param name="image">Картинка.</param>
        public FindImageResponse(byte[] image)
        {
            this.Image = image;
        }

        /// <summary>
        /// Gets or sets картинка, если найдена. Null, если не найдена.
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindImageResponse"/> class.
        /// </summary>
        /// <param name="image">Картинка.</param>
        /// <returns>A new instance of the <see cref="FindImageResponse"/> class.</returns>
        public static FindImageResponse Create(byte[] image)
            => new FindImageResponse(image);
    }
}
