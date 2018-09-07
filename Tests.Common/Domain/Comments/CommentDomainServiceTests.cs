using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Comments;
using Infotecs.MiniJournal.Domain.Comments.Exceptions;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Common.Domain.Comments
{
    [TestFixture]
    public class CommentDomainServiceTests
    {
        private IFixture fixture;

        // frozen dependencies
        private ICommentRepository repository;

        // services under test
        private CommentDomainService service;

        [SetUp]
        public void SetUp()
        {
            this.fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

            // freeze dependencies
            this.repository = this.fixture.Freeze<ICommentRepository>();

            // create services under test
            this.service = this.fixture.Create<CommentDomainService>();
        }

        [Test]
        public async Task ShouldAddCommentToArticleCommentsCollection()
        {
            // Arrange
            var article = this.fixture.Create<Article>();
            var comment = this.fixture.Create<Comment>();

            // Act
            await this.service.AddCommentAsync(article, comment);

            // Assert
            article.Comments.Should().Contain(comment);
        }

        [Test]
        public async Task ShouldAddCommentToRepository()
        {
            // Arrange
            var article = this.fixture.Create<Article>();
            var comment = this.fixture.Create<Comment>();

            // Act
            await this.service.AddCommentAsync(article, comment);

            // Assert
            Received.InOrder(async () =>
            {
                await this.repository.AddAsync(Arg.Is(article.Id), Arg.Is(comment));
            });
        }

        [Test]
        public async Task ShouldDeleteCommentFromRepository()
        {
            // Arrange
            var article = this.fixture.Create<Article>();
            var comment = this.fixture.Create<Comment>();

            // Act
            await this.service.DeleteCommentAsync(article, comment);

            // Assert
            Received.InOrder(async () =>
            {
                await this.repository.DeleteAsync(Arg.Is(article.Id), Arg.Is(comment));
            });
        }

        [Test]
        public async Task ShouldDeleteCommentFromArticleCommentsCollection()
        {
            // Arrange
            var article = this.fixture.Create<Article>();
            var comment = article.Comments.First();

            // Act
            await this.service.DeleteCommentAsync(article, comment);

            // Assert
            article.Comments.Should().NotContain(comment);
        }

        [Test]
        public void ShouldThrowArticleNotFoundExceptionIfArticleNotFound()
        {
            // Arrange            
            var article = this.fixture.Create<Article>();
            var anotherCommentId = this.fixture.Create<long>();

            // Act 
            Func<Task<Comment>> act = async () => await this.service.GetCommentById(article, anotherCommentId);

            act.Should().Throw<CommentNotFoundException>();
        }

        [Test]
        public async Task ShouldSuccessfullyReturnArticle()
        {
            // Arrange            
            var article = this.fixture.Create<Article>();
            var comment = article.Comments.First();

            // Act
            var result = await this.service.GetCommentById(article, comment.Id);

            // Assert
            result.Should().BeSameAs(comment);
        }
    }
}
