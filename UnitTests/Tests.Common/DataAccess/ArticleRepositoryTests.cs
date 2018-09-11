using System;
using System.Collections.Generic;
using System.Data;
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
using FluentNHibernate.Cfg;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Comments;
using Infotecs.MiniJournal.Domain.Users;
using Infotecs.MiniJournal.PostgreSql.NHibernate;
using Infotecs.MiniJournal.PostgreSql.NHibernate.Mappings;
using NHibernate;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Common.DataAccess
{
    [TestFixture]
    public class ArticleRepositoryTests
    {
        private IFixture fixture;
        private SessionProvider sessionProvider;

        [SetUp]
        public async Task SetUp()
        {
            this.fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

            await this.CreateInMemoryDatabase();
        }

        private async Task CreateInMemoryDatabase()
        {
            var sessionFactory = Fluently.Configure()
                .Database(
                    FluentNHibernate.Cfg.Db.SQLiteConfiguration.Standard.InMemory()
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ArticleMap>())
                .BuildSessionFactory();

            this.sessionProvider = new SessionProvider(sessionFactory);
            await SeedTestData(this.sessionProvider.GetSession().Connection);
                       
            this.fixture.Register<ISessionProvider>(() => this.sessionProvider);
        }

        private static async Task SeedTestData(IDbConnection connection)
        {
            var scriptsDirectory = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\Database");

            var createDbScript = File.ReadAllText(Path.Combine(scriptsDirectory, "create_db.sql"));

            // replace postrgres specific syntax with sqlite specific
            createDbScript = Regex.Replace(createDbScript, "bigint(.*?)generated always as identity", "INTEGER$1AUTOINCREMENT");
            await connection.ExecuteAsync(createDbScript);
            await connection.ExecuteAsync(File.ReadAllText(Path.Combine(scriptsDirectory, "insert_test_users.sql")));
            await connection.ExecuteAsync(File.ReadAllText(Path.Combine(scriptsDirectory, "insert_test_articles.sql")));
            await connection.ExecuteAsync(File.ReadAllText(Path.Combine(scriptsDirectory, "insert_test_comments.sql")));
        }

        [TearDown]
        public void TearDown()
        {
            this.sessionProvider?.Dispose();
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
                article.User.Id.Should().Be(article.User.Id);
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
            
            var article = await repository.FindByIdAsync(articleId);
            article.Should().NotBeNull();

            // Act
            await repository.DeleteAsync(article);

            // Assert
            article = await repository.FindByIdAsync(articleId);
            article.Should().BeNull();
        }

        [Test]
        public async Task AddTest()
        {
            // Arrange 
            var repository = this.fixture.Create<ArticleRepository>();
            var userRepository = this.fixture.Create<UserRepository>();

            var user = await userRepository.FindByIdAsync(1);
            user.Should().NotBeNull();

            var articleText = this.fixture.Create<string>();
            var article = new Article(user,  articleText, new List<Comment>());

            // Act 
            await repository.AddAsync(article);

            // Assert
            var result = await repository.FindByIdAsync(article.Id);
            result.Should().NotBeNull();
        }
    }
}
