using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Articles;
using NHibernate.Linq;

namespace Infotecs.MiniJournal.PostgreSql.NHibernate
{
    /// <inheritdoc cref="IArticleRepository"/>
    internal class ArticleRepository : BaseNHibernateRepository, IArticleRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">Implementation of <see cref="ISessionProvider"/>.</param>
        public ArticleRepository(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }

        private IQueryable<Article> Articles
            => this.Session.Query<Article>()
                .Fetch(x => x.User)
                .FetchMany(x => x.Comments)
                .ThenFetch(c => c.User);

        /// <inheritdoc />
        public async Task<IEnumerable<Article>> GetArticlesAsync()
        {
            return await this.Articles.ToListAsync();
        }

        /// <inheritdoc />
        public Task<Article> FindByIdAsync(long articleId)
        {
            return this.Session.GetAsync<Article>(articleId);
        }

        /// <inheritdoc />
        public Task DeleteAsync(Article article)
        {
            return this.Session.DeleteAsync(article);
        }

        /// <inheritdoc />
        public Task AddAsync(Article article)
        {
            return this.Session.SaveAsync(article);
        }
    }
}
