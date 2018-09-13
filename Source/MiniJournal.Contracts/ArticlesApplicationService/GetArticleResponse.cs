using System;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService.Entities;

namespace Infotecs.MiniJournal.Contracts.ArticlesApplicationService
{
    /// <summary>
    /// Результат запроса одной статьи. 
    /// </summary>
    public class GetArticleResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetArticleResponse"/> class.
        /// </summary>
        public GetArticleResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetArticleResponse"/> class.
        /// </summary>
        /// <param name="article">Статья.</param>
        public GetArticleResponse(Article article)
        {
            this.Article = article;
        }

        /// <summary>
        /// Статья с комментариями.
        /// </summary>
        public Article Article { get; set; }
    }
}
