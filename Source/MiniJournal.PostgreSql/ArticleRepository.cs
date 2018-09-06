using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Comments;
using Infotecs.MiniJournal.Domain.Users;

namespace MiniJournal.PostgreSql
{
    internal class ArticleRepository : IArticleRepository
    {
        private readonly IDbConnectionFactory connectionFactory;

        public ArticleRepository(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
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
            using (var connection = this.connectionFactory.Create())
            {
                await connection.ExecuteAsync("DELETE FROM articles WHERE id = @id", article);
            }
        }

        public async Task AddAsync(Article article)
        {
            using (var connection = this.connectionFactory.Create())
            {
                await connection.ExecuteAsync("INSERT INTO articles (text, image_id, user_id) VALUES (@Text, @ImageId, @UserId)", article);
            }
        }

        private async Task<List<Article>> SelectArticles(string where = null, object param = null)
        {
            var articles = new List<Article>();

            using (var connection = this.connectionFactory.Create())
            {
                IEnumerable<Article> dbArticles = await connection.QueryAsync<Article>($"SELECT a.* FROM articles a {where}", param: param);

                IEnumerable<User> dbArticleUsers = await connection.QueryAsync<User>(
                    "SELECT u.* FROM users u WHERE u.id IN @users",
                    new { @users = dbArticles.Select(x => x.UserId).Distinct() });

                IEnumerable<Comment> dbComments = await connection.QueryAsync<Comment>(
                    "SELECT c.* FROM comments c WHERE c.article_id IN @articles",
                    new { articles = dbArticles.Select(a => a.Id) });

                IEnumerable<User> dbCommentUsers = await connection.QueryAsync<User>(
                    "SELECT u.* FROM users u WHERE u.id IN @users",
                    new { @users = dbComments.Select(x => x.UserId).Distinct() });

                ILookup<long, User> users = dbArticleUsers.Concat(dbCommentUsers).ToLookup(x => x.Id);

                foreach (var dbArticle in dbArticles)
                {
                    var articleComments = new List<Comment>();

                    IEnumerable<Comment> dbArticleComments = dbComments.Where(x => x.ArticleId == dbArticle.Id);

                    foreach (var dbComment in dbArticleComments)
                    {
                        var commentUser = users[dbComment.UserId].First();
                        var comment = new Comment(commentUser, dbArticle, dbComment.Text);

                        articleComments.Add(comment);
                    }

                    var articleUser = users[dbArticle.UserId].First();

                    var article = new Article(articleUser, dbArticle.Text, articleComments)
                    {
                        ImageId = dbArticle.ImageId
                    };

                    articles.Add(article);
                }
            }

            return articles;
        }
    }
}
