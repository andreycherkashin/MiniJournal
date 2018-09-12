using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Users;
using NUnit.Framework;

namespace Tests.Common.Domain.Articles
{
    /// <summary>
    /// Тесты для <see cref="ArticleFactory"/>.
    /// </summary>
    [TestFixture]
    public class ArticleFactoryTests
    {
        private IFixture fixture;

        // services under test
        private ArticleFactory factory;

        /// <summary>
        /// Настройка тестового окружения.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

            // create services under test
            this.factory = this.fixture.Create<ArticleFactory>();
        }

        /// <summary>
        /// Статья должна быть успешно создана.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task ShouldCreateArticle()
        {
            // Arrange
            var text = this.fixture.Create<string>();
            var imageId = this.fixture.Create<string>();
            var user = this.fixture.Create<User>();

            // Act
            Article article = await this.factory.CreateAsync(text, imageId, user);

            // Assert
            article.Should().NotBeNull();
            article.User.Should().BeSameAs(user);
            article.ImageId.Should().Be(imageId);
            article.Text.Should().Be(text);
            article.Comments.Should().BeNullOrEmpty();
        }
    }
}
