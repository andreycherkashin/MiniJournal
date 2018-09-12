using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Dapper;
using FluentAssertions;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Comments;
using Infotecs.MiniJournal.Domain.Users;
using Infotecs.MiniJournal.PostgreSql.NHibernate;
using Infotecs.MiniJournal.PostgreSql.NHibernate.Mappings;
using NHibernate;
using NUnit.Framework;

namespace Tests.Common.DataAccess
{
    /// <summary>
    /// Интеграционный тест репозитория статей с InMemory базой данных Sqlite.
    /// </summary>
    [TestFixture]
    public class ArticleRepositoryTests
    {
        private IFixture fixture;
        private SessionProvider sessionProvider;

        /// <summary>
        /// Настройка тестового окружения.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [SetUp]
        public async Task SetUp()
        {
            this.fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

            await this.CreateInMemoryDatabase();
        }

        /// <summary>
        /// Очистка тестового окружения после теста.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.sessionProvider?.Dispose();
        }

        /// <summary>
        /// Тест метода <see cref="IArticleRepository.AddAsync"/>.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task AddTest()
        {
            // Arrange
            var repository = this.fixture.Create<ArticleRepository>();
            var userRepository = this.fixture.Create<UserRepository>();

            User user = await userRepository.FindByIdAsync(1);
            user.Should().NotBeNull();

            var articleText = this.fixture.Create<string>();
            var article = new Article(user, articleText, new List<Comment>());

            // Act
            await repository.AddAsync(article);

            // Assert
            Article result = await repository.FindByIdAsync(article.Id);
            result.Should().NotBeNull();
        }

        /// <summary>
        /// Тест метода <see cref="IArticleRepository.DeleteAsync"/>.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task DeleteTest()
        {
            // Arrange
            var repository = this.fixture.Create<ArticleRepository>();
            var articleId = 1;

            Article article = await repository.FindByIdAsync(articleId);
            article.Should().NotBeNull();

            // Act
            await repository.DeleteAsync(article);

            // Assert
            article = await repository.FindByIdAsync(articleId);
            article.Should().BeNull();
        }

        /// <summary>
        /// Тест метода <see cref="IArticleRepository.FindByIdAsync"/>.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task FindByIdTest()
        {
            // Arrange
            var repository = this.fixture.Create<ArticleRepository>();

            // Act
            Article article = await repository.FindByIdAsync(1);

            // Assert
            article.Should().NotBeNull();
        }

        /// <summary>
        /// Тест метода <see cref="IArticleRepository.GetArticlesAsync"/>.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task GetArticlesTest()
        {
            // Arrange
            var repository = this.fixture.Create<ArticleRepository>();

            // Act
            List<Article> articles = (await repository.GetArticlesAsync()).OrderBy(x => x.Id).ToList();

            // Assert
            articles.Should().NotBeEmpty();
            articles.Should().HaveCount(2);

            foreach (Article article in articles)
            {
                article.Should().NotBeNull();
                article.Comments.Should().HaveCount(2);
                article.User.Should().NotBeNull();
                article.User.Name.Should().NotBeEmpty();
                article.Text.Should().NotBeEmpty();
                article.User.Id.Should().Be(article.User.Id);
            }
        }

        private static async Task SeedTestData(IDbConnection connection)
        {
            string scriptsDirectory = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\Database");

            string createDbScript = File.ReadAllText(Path.Combine(scriptsDirectory, "create_db.sql"));

            // replace postrgres specific syntax with sqlite specific
            createDbScript = Regex.Replace(createDbScript, "bigint(.*?)generated always as identity", "INTEGER$1AUTOINCREMENT");
            await connection.ExecuteAsync(createDbScript);
            await connection.ExecuteAsync(File.ReadAllText(Path.Combine(scriptsDirectory, "insert_test_users.sql")));
            await connection.ExecuteAsync(File.ReadAllText(Path.Combine(scriptsDirectory, "insert_test_articles.sql")));
            await connection.ExecuteAsync(File.ReadAllText(Path.Combine(scriptsDirectory, "insert_test_comments.sql")));
        }

        private async Task CreateInMemoryDatabase()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(
                    SQLiteConfiguration.Standard.InMemory())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ArticleMap>())
                .BuildSessionFactory();

            this.sessionProvider = new SessionProvider(sessionFactory);
            await SeedTestData(this.sessionProvider.GetSession().Connection);

            this.fixture.Register<ISessionProvider>(() => this.sessionProvider);
        }
    }
}
