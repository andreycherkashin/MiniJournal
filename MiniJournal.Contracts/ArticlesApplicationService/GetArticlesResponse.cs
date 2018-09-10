using System;
using System.Collections.Generic;
using System.Text;
using Infotecs.MiniJournal.Domain.Articles;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    public class GetArticlesResponse
    {
        public List<Article> Articles { get; set; }
    }
}
