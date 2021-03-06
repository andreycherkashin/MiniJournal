﻿using System;
using System.Collections.Generic;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService.Entities;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    /// <summary>
    /// Результат запросы списка статей.
    /// </summary>
    public class GetArticlesResponse
    {
        /// <summary>
        /// Gets or sets список статей.
        /// </summary>
        public List<Article> Articles { get; set; }
    }
}
