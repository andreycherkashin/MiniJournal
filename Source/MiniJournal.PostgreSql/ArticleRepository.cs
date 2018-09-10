using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Comments;
using Infotecs.MiniJournal.Domain.Users;

namespace Infotecs.MiniJournal.PostgreSql
{
    internal class ArticleRepository : IArticleRepository
    {
        private readonly IDbConnectionFactory connectionFactory;
        private readonly IDbEngineStatementsProvider dbEngineStatementsProvider;

        public ArticleRepository(IDbConnectionFactory connectionFactory, IDbEngineStatementsProvider dbEngineStatementsProvider)
        {
            this.connectionFactory = connectionFactory;
            this.dbEngineStatementsProvider = dbEngineStatementsProvider;
        }

        public async Task<IEnumerable<Article>> GetArticlesAsync()
        {
            return await this.SelectArticles();
        }

        public async Task<Article> FindByIdAsync(long articleId)
        {
            List<Article> articles = await this.SelectArticles("WHERE a.id = @id", new { id = articleId });
            return articles.FirstOrDefault();
        }

        public async Task DeleteAsync(Article article)
        {
            var connection = this.connectionFactory.GetConnection();
            await connection.ExecuteAsync("DELETE FROM articles WHERE id = @id", article);
        }

        public async Task AddAsync(Article article)
        {
            var connection = this.connectionFactory.GetConnection();

            var id = await connection.ExecuteScalarAsync<long>(
                $"INSERT INTO articles (text, image_id, user_id) VALUES (@Text, @ImageId, @UserId) {this.dbEngineStatementsProvider.GetLastInsertedIdSelectStatement()}", 
                article);

            article.Id = id;
        }

        private async Task<List<Article>> SelectArticles(string where = null, object param = null)
        {
            var articles = new List<Article>();

            var connection = this.connectionFactory.GetConnection();
            
            List<Article> dbArticles = (await connection.QueryAsync<Article>($"SELECT a.* FROM articles a {where}", param: param)).ToList();

            if (!dbArticles.Any())
            {
                return new List<Article>();
            }

            List<User> dbArticleUsers = (await connection.QueryAsync<User>(
                "SELECT u.* FROM users u WHERE u.id IN @users ",
                new { users = dbArticles.Select(x => x.UserId).Distinct().ToList() })).ToList();

            List<Comment> dbComments = (await connection.QueryAsync<Comment>(
                "SELECT c.* FROM comments c WHERE c.article_id IN @articles ",
                new { articles = dbArticles.Select(a => a.Id).ToList() })).ToList();

            List<User> dbCommentUsers;

            if (dbComments.Any())
            {
                dbCommentUsers = (await connection.QueryAsync<User>(
                    "SELECT u.* FROM users u WHERE u.id IN @users ",
                    new { users = dbComments.Select(x => x.UserId).Distinct().ToList() })).ToList();
            }
            else
            {
                dbCommentUsers = new List<User>();
            }

            ILookup<long, User> users = dbArticleUsers.Concat(dbCommentUsers).ToLookup(x => x.Id);

            foreach (var dbArticle in dbArticles)
            {
                var articleComments = new List<Comment>();

                IEnumerable<Comment> dbArticleComments = dbComments.Where(x => x.ArticleId == dbArticle.Id);

                foreach (var dbComment in dbArticleComments)
                {
                    var commentUser = users[dbComment.UserId].First();
                    var comment = new Comment(commentUser, dbArticle.Id, dbComment.Text)
                    {
                        Id = dbComment.Id
                    };

                    articleComments.Add(comment);
                }

                var articleUser = users[dbArticle.UserId].First();

                var article = new Article(articleUser, dbArticle.Text, articleComments)
                {
                    Id = dbArticle.Id,
                    ImageId = dbArticle.ImageId
                };

                articles.Add(article);
            }

            return articles;
        }
    }
}
