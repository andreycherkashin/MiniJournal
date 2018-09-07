﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Comments;
using Infotecs.MiniJournal.Domain.Users;

namespace MiniJournal.Application
{
    /// <summary>
    /// Класс реализует различные высокоуровневые операции над статьей.
    /// </summary>
    internal class ArticlesService : IArticlesService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IArticleFactory articleFactory;
        private readonly IArticleRepository articleRepository;
        private readonly IArticleDomainService articleService;
        private readonly ICommentFactory commentFactory;
        private readonly ICommentDomainService commentService;
        private readonly IUserDomainService userService;

        public ArticlesService(
            IUnitOfWork unitOfWork,
            IArticleFactory articleFactory,
            IArticleRepository articleRepository,
            IArticleDomainService articleService,
            ICommentFactory commentFactory,
            ICommentDomainService commentService,
            IUserDomainService userService)
        {
            this.unitOfWork = unitOfWork;
            this.articleFactory = articleFactory;
            this.articleRepository = articleRepository;
            this.articleService = articleService;
            this.commentFactory = commentFactory;
            this.commentService = commentService;
            this.userService = userService;
        }

        /// <summary>
        /// Возвращает список всех имеющихся статей
        /// </summary>
        /// <returns>Список всех статей</returns>
        public Task<IEnumerable<Article>> GetArticlesAsync()
        {
            return this.articleRepository.GetArticlesAsync();
        }

        /// <summary>
        /// Создать статью с указанным содержимым.
        /// </summary>
        /// <param name="text">Содержимое статьи.</param>
        /// <param name="image">Картинка.</param>
        /// <param name="userId">Идентификатор пользователя создавшего статью.</param>
        public async Task CreateArticleAsync(string text, byte[] image, long userId)
        {
            var user = await this.userService.GetUserByIdAsync(userId);
            var imageId = await this.imageService.UploadImage(image);
            var article = await this.articleFactory.CreateArticleAsync(text, imageId, user);

            await this.articleRepository.AddAsync(article);
        }

        /// <summary>
        /// Удаляет статью.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        public async Task DeleteArticleAsync(long articleId)
        {
            var article = await this.articleService.GetArticleByIdAsync(articleId);
            await this.articleService.DeleteArticleAsync(article);

            await this.unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Добавляет комментарий к статье.
        /// </summary>
        /// <param name="text">Содержимое комментария.</param>
        /// <param name="userId">Идентификатор пользователя добавившего комментарий.</param>
        /// <param name="articleId">Идентификатор статьи, к которой добавляется комментарий.</param>
        public async Task AddCommentAsync(string text, long userId, long articleId)
        {
            var user = await this.userService.GetUserByIdAsync(userId);
            var article = await this.articleService.GetArticleByIdAsync(articleId);
            var comment = await this.commentFactory.CreateAsync(text, user, article);

            await this.commentService.AddCommentAsync(article, comment);

            await this.unitOfWork.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет комментарий.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <param name="commentId">Идентификатор комментария.</param>
        public async Task DeleteCommentAsync(long articleId, long commentId)
        {
            var article = await this.articleService.GetArticleByIdAsync(articleId);
            var comment = await this.commentService.GetCommentById(article, commentId);

            await this.commentService.DeleteCommentAsync(article, comment);

            await this.unitOfWork.SaveChangesAsync();
        }
    }
}