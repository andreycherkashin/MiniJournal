using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Users;
using NUnit.Framework;

namespace Tests.Common.Domain.Articles
{
    [TestFixture]
    public class ArticleFactoryTests
    {
        private IFixture fixture;

        // services under test
        private ArticleFactory factory;

        [SetUp]
        public void SetUp()
        {
            this.fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            
            // create services under test
            this.factory = this.fixture.Create<ArticleFactory>();
        }

        [Test]
        public async Task ShouldCreateArticle()
        {
            // Arrange
            var text = this.fixture.Create<string>();
            var imageId = this.fixture.Create<string>();
            var user = this.fixture.Create<User>();
            
            // Act
            var article = await this.factory.CreateAsync(text, imageId, user);

            // Assert
            article.Should().NotBeNull();
            article.User.Should().BeSameAs(user);            
            article.ImageId.Should().Be(imageId);
            article.Text.Should().Be(text);
            article.Comments.Should().BeNullOrEmpty();
        }
    }
}
