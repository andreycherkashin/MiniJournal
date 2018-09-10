using System;
using System.Collections.Generic;
using System.Text;
using Infotecs.MiniJournal.Domain.Articles;

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
