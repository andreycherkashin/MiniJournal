﻿using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Articles.Exceptions;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Common.Domain.Articles
{
    /// <summary>
    /// Тесты сервиса <see cref="ArticleDomainService"/>.
    /// </summary>
    [TestFixture]
    public class ArticleDomainServiceTests
    {
        private IFixture fixture;

        // frozen dependencies
        private IArticleRepository repository;

        // services under test
        private ArticleDomainService service;

        /// <summary>
        /// Настройка тестового окружения.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.fixture = new Fixture().Customize(new AutoNSubstituteCustomization());

            // freeze dependencies
            this.repository = this.fixture.Freeze<IArticleRepository>();

            // create services under test
            this.service = this.fixture.Create<ArticleDomainService>();
        }

        /// <summary>
        /// Статья должна быть добавлена в репозиторий.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task ShouldAddArticleToRepository()
        {
            // Arrange
            var article = this.fixture.Create<Article>();

            // Act
            await this.service.CreateArticleAsync(article);

            // Assert
            Received.InOrder(async () => { await this.repository.AddAsync(Arg.Is(article)); });
        }

        /// <summary>
        /// Метод репозитория должен быть вызван при поиске статьи.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task ShouldCallRepositoryForArticle()
        {
            // Arrange            
            var article = this.fixture.Create<Article>();
            this.repository.FindByIdAsync(Arg.Is(article.Id)).Returns(Task.FromResult(article));

            // Act
            await this.service.GetArticleByIdAsync(article.Id);

            // Assert
            Received.InOrder(async () => { await this.repository.FindByIdAsync(Arg.Is(article.Id)); });
        }

        /// <summary>
        /// Статья должна удаляться через репозиторий.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task ShouldDeleteArticleFromRepository()
        {
            // Arrange
            var article = this.fixture.Create<Article>();

            // Act
            await this.service.DeleteArticleAsync(article);

            // Assert
            Received.InOrder(async () => { await this.repository.DeleteAsync(Arg.Is(article)); });
        }

        /// <summary>
        /// Статья должна быть успешно найдена.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task ShouldSuccessfullyReturnArticle()
        {
            // Arrange            
            var article = this.fixture.Create<Article>();
            this.repository.FindByIdAsync(Arg.Is(article.Id)).Returns(Task.FromResult(article));

            // Act
            Article result = await this.service.GetArticleByIdAsync(article.Id);

            // Assert
            result.Should().BeSameAs(article);
        }

        /// <summary>
        /// Должно быть выброшено исключение <see cref="ArticleNotFoundException"/>, если статья не найдена.
        /// </summary>
        [Test]
        public void ShouldThrowArticleNotFoundExceptionIfArticleNotFound()
        {
            // Arrange            
            var article = this.fixture.Create<Article>();
            this.repository.FindByIdAsync(Arg.Any<long>()).Returns(Task.FromResult<Article>(null));
            this.repository.FindByIdAsync(Arg.Is(article.Id)).Returns(Task.FromResult(article));

            var anotherArticleId = this.fixture.Create<long>();

            // Act 
            Func<Task<Article>> act = async () => await this.service.GetArticleByIdAsync(anotherArticleId);

            act.Should().Throw<ArticleNotFoundException>();
        }
    }
}
