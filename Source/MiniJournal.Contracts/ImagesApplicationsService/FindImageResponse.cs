﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Infotecs.MiniJournal.Contracts.ImagesApplicationsService
{
    /// <summary>
    /// Результат запроса поиска картинки.
    /// </summary>
    public class FindImageResponse
    {
        public FindImageResponse()
        {
        }

        public FindImageResponse(byte[] image)
        {
            this.Image = image;
        }

        /// <summary>
        /// Картинка, если найдена. Null, если не найдена.
        /// </summary>
        public byte[] Image { get; set; }
    }
}