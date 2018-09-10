using System;
using System.Collections.Generic;
using System.Text;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService.Entites;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    /// <summary>
    /// Результат запросы списка статей.
    /// </summary>
    public class GetArticlesResponse
    {
        /// <summary>
        /// Список статей.
        /// </summary>
        public List<Article> Articles { get; set; }
    }
}
