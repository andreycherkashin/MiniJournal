using System;
using System.Linq;
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
    /// <summary>
    /// Тесты для <see cref="CommentDomainService"/>.
    /// </summary>
    [TestFixture]
    public class CommentDomainServiceTests
    {
        private IFixture fixture;

        // frozen dependencies
        private ICommentRepository repository;

        // services under test
        private CommentDomainService service;

        /// <summary>
        /// Настройка тестового окружения.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            this.fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            this.fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            // freeze dependencies
            this.repository = this.fixture.Freeze<ICommentRepository>();

            // create services under test
            this.service = this.fixture.Create<CommentDomainService>();
        }

        /// <summary>
        /// Комментарий должен быть добавлен в список комментариев статьи.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
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

        /// <summary>
        /// Комментарий должен быть добавлен в репозиторий комментариев.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task ShouldAddCommentToRepository()
        {
            // Arrange
            var article = this.fixture.Create<Article>();
            var comment = this.fixture.Create<Comment>();

            // Act
            await this.service.AddCommentAsync(article, comment);

            // Assert
            Received.InOrder(async () => { await this.repository.AddAsync(Arg.Is(article.Id), Arg.Is(comment)); });
        }

        /// <summary>
        /// Комментарий должен удаляться из коллекции комментариев статьи.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task ShouldDeleteCommentFromArticleCommentsCollection()
        {
            // Arrange
            var article = this.fixture.Create<Article>();
            Comment comment = article.Comments.First();

            // Act
            await this.service.DeleteCommentAsync(article, comment);

            // Assert
            article.Comments.Should().NotContain(comment);
        }

        /// <summary>
        /// Комментарий должен быть удален из репозитория.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task ShouldDeleteCommentFromRepository()
        {
            // Arrange
            var article = this.fixture.Create<Article>();
            var comment = this.fixture.Create<Comment>();

            // Act
            await this.service.DeleteCommentAsync(article, comment);

            // Assert
            Received.InOrder(async () => { await this.repository.DeleteAsync(Arg.Is(article.Id), Arg.Is(comment)); });
        }


        /// <summary>
        /// Комментарий должен быть успешно найден и возвращен.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task ShouldSuccessfullyReturnComment()
        {
            // Arrange            
            var article = this.fixture.Create<Article>();
            Comment comment = article.Comments.First();

            // Act
            Comment result = await this.service.GetCommentById(article, comment.Id);

            // Assert
            result.Should().BeSameAs(comment);
        }

        /// <summary>
        /// Должно быть выброшено исключение <see cref="CommentNotFoundException"/>, если комментарий не найден.
        /// </summary>
        [Test]
        public void ShouldThrowCommentNotFoundException()
        {
            // Arrange            
            var article = this.fixture.Create<Article>();
            var anotherCommentId = this.fixture.Create<long>();

            // Act 
            Func<Task<Comment>> act = async () => await this.service.GetCommentById(article, anotherCommentId);

            act.Should().Throw<CommentNotFoundException>();
        }
    }
}
