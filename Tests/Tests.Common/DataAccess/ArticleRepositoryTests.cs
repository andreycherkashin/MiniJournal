using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Dapper;
using FluentAssertions;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Comments;
using Infotecs.MiniJournal.Domain.Users;
using MiniJournal.PostgreSql;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Common.DataAccess
{
    [TestFixture]
    public class ArticleRepositoryTests
    {
        private IFixture fixture;
        private SQLiteConnection connection;

        [SetUp]
        public async Task SetUp()
        {
            this.fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

            this.CreateInMemoryDatabase();
            await this.SeedTestData();
            this.CreateSqliteStatementsProvider();

            PostgreSqlModule.InitializeMappings();
        }

        private void CreateSqliteStatementsProvider()
        {
            var sqliteStatementsProvider = Substitute.For<IDbEngineStatementsProvider>();
            sqliteStatementsProvider.GetLastInsertedIdSelectStatement().Returns("; select last_insert_rowid() as id;");
            this.fixture.Register<IDbEngineStatementsProvider>(() => sqliteStatementsProvider);
        }

        private void CreateInMemoryDatabase()
        {
            this.connection = new SQLiteConnection("Data Source=:memory:");
            this.connection.Open();

            var inMemoryDatabaseConnectionFactory = Substitute.For<IDbConnectionFactory>();
            inMemoryDatabaseConnectionFactory.GetConnection().Returns(this.connection);

            this.fixture.Register<IDbConnectionFactory>(() => inMemoryDatabaseConnectionFactory);
        }

        private async Task SeedTestData()
        {
            var scriptsDirectory = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\Database");

            var createDbScript = File.ReadAllText(Path.Combine(scriptsDirectory, "create_db.sql"));

            // remove postrgres specific syntax with sqlite specific
            createDbScript = Regex.Replace(createDbScript, "bigint(.*?)generated always as identity", "INTEGER$1AUTOINCREMENT");
            await this.connection.ExecuteAsync(createDbScript);

            await this.connection.ExecuteAsync(File.ReadAllText(Path.Combine(scriptsDirectory, "insert_test_users.sql")));
            await this.connection.ExecuteAsync(File.ReadAllText(Path.Combine(scriptsDirectory, "insert_test_articles.sql")));
            await this.connection.ExecuteAsync(File.ReadAllText(Path.Combine(scriptsDirectory, "insert_test_comments.sql")));
        }

        [TearDown]
        public void TearDown()
        {
            this.connection?.Dispose();
        }

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

            foreach (var article in articles)
            {
                article.Should().NotBeNull();
                article.Comments.Should().HaveCount(2);
                article.User.Should().NotBeNull();
                article.User.Name.Should().NotBeEmpty();
                article.Text.Should().NotBeEmpty();
                article.UserId.Should().Be(article.User.Id);
            }                       
        }


        [Test]
        public async Task FindByIdTest()
        {
            // Arrange
            var repository = this.fixture.Create<ArticleRepository>();

            // Act
            var article = await repository.FindByIdAsync(1);

            // Assert
            article.Should().NotBeNull();
        }

        [Test]
        public async Task DeleteTest()
        {
            // Arrange
            var repository = this.fixture.Create<ArticleRepository>();
            var articleId = 1;

            // Act
            var article = await repository.FindByIdAsync(articleId);
            article.Should().NotBeNull();

            await repository.DeleteAsync(article);

            // Assert
            article = await repository.FindByIdAsync(articleId);
            article.Should().BeNull();
        }

        [Test]
        public async Task AddTest()
        {
            // arrange 
            var repository = this.fixture.Create<ArticleRepository>();
            var userRepository = this.fixture.Create<UserRepository>();

            var user = await userRepository.FindByIdAsync(1);
            user.Should().NotBeNull();

            var articleText = this.fixture.Create<string>();
            var article = new Article(user,  articleText, new List<Comment>());

            // Act 
            await repository.AddAsync(article);            

            // Arrange
            var result = await repository.FindByIdAsync(article.Id);
            result.Should().NotBeNull();
        }
    }
}
