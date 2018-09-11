using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Articles;
using NHibernate.Linq;

namespace Infotecs.MiniJournal.PostgreSql.NHibernate
{
    internal class ArticleRepository : BaseNHibernateRepository, IArticleRepository
    {        
        public ArticleRepository(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }

        private IQueryable<Article> Articles
            => this.Session.Query<Article>()
                .Fetch(x => x.User)
                .FetchMany(x => x.Comments)
                    .ThenFetch(c => c.User);

        /// <summary>
        /// Возвращает список имеющихся статей.
        /// </summary>
        /// <returns>Список статей.</returns>
        public async Task<IEnumerable<Article>> GetArticlesAsync()
            => await this.Articles.ToListAsync();

        /// <summary>
        /// Находит статью по идентификатору. Если статья не найдена, возвращается null.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <returns>Статью, либо null, если не найдена.</returns>
        public Task<Article> FindByIdAsync(long articleId)
            => this.Session.GetAsync<Article>(articleId);

        /// <summary>
        /// Удаляет статью.
        /// </summary>
        /// <param name="article">Статья.</param>
        public Task DeleteAsync(Article article)
            => this.Session.DeleteAsync(article);

        /// <summary>
        /// Добавляет статью.
        /// </summary>
        /// <param name="article">Статья.</param>
        public Task AddAsync(Article article)
            => this.Session.SaveAsync(article);
    }
}
