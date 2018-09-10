using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.Contracts.ImagesApplicationsService;
using Infotecs.MiniJournal.Domain.Articles;
using Infotecs.MiniJournal.Domain.Comments;
using Infotecs.MiniJournal.Domain.Users;
using Microsoft.Toolkit.Extensions;
using Serilog;

namespace Infotecs.MiniJournal.Application
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
        private readonly IImagesService imagesService;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public ArticlesService(
            IUnitOfWork unitOfWork,
            IArticleFactory articleFactory,
            IArticleRepository articleRepository,
            IArticleDomainService articleService,
            ICommentFactory commentFactory,
            ICommentDomainService commentService,
            IUserDomainService userService,
            IImagesService imagesService,
            ILogger logger,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.articleFactory = articleFactory;
            this.articleRepository = articleRepository;
            this.articleService = articleService;
            this.commentFactory = commentFactory;
            this.commentService = commentService;
            this.userService = userService;
            this.imagesService = imagesService;
            this.logger = logger;
            this.mapper = mapper;
        }

        /// <summary>
        /// Возвращает список всех имеющихся статей
        /// </summary>
        /// <returns>Список всех статей</returns>
        public async Task<GetArticlesResponse> GetArticlesAsync(GetArticlesRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            IEnumerable<Article> articles = await this.articleRepository.GetArticlesAsync();

            return new GetArticlesResponse
            {
                Articles = this.mapper.Map<List<Contracts.ArticlesApplicationService.Entites.Article>>(articles)
            };
        }

        /// <summary>
        /// Создать статью с указанным содержимым.
        /// </summary>
        /// <param name="request">Запрос создания статьи.</param>
        public async Task<CreateArticleResponse> CreateArticleAsync(CreateArticleRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var user = await this.userService.GetUserByIdAsync(request.UserId);
            var createImageResponse = await this.imagesService.UploadImageAsync(new UploadImageRequest { Image = request.Image });
            var article = await this.articleFactory.CreateAsync(request.Text, createImageResponse.ImageId, user);

            await this.articleService.CreateArticleAsync(article);

            await this.unitOfWork.SaveChangesAsync();

            this.logger.Verbose("user {UserId} created new article: {ArticleText}", request.UserId, article.Text.Truncate(30, true));

            return  new CreateArticleResponse(true);
        }

        /// <summary>
        /// Удаляет статью.
        /// </summary>
        /// <param name="request">Запрос удаления статьи.</param>
        public async Task<DeleteArticleResponse> DeleteArticleAsync(DeleteArticleRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var article = await this.articleService.GetArticleByIdAsync(request.ArticleId);
            await this.articleService.DeleteArticleAsync(article);

            await this.unitOfWork.SaveChangesAsync();

            this.logger.Verbose("article {ArticleId} was deleted", request.ArticleId);

            return new DeleteArticleResponse(true);
        }

        /// <summary>
        /// Добавляет комментарий к статье.
        /// </summary>
        /// <param name="request">Запрос добавления статьи.</param>        
        public async Task<AddCommentResponse> AddCommentAsync(AddCommentRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var user = await this.userService.GetUserByIdAsync(request.UserId);
            var article = await this.articleService.GetArticleByIdAsync(request.ArticleId);
            var comment = await this.commentFactory.CreateAsync(request.Text, user, article);

            await this.commentService.AddCommentAsync(article, comment);

            await this.unitOfWork.SaveChangesAsync();

            this.logger.Verbose("new comment added to article {ArticleId}, comment text: {CommentText}", request.ArticleId, comment.Text.Truncate(30, true));

            return new AddCommentResponse(true);
        }

        /// <summary>
        /// Удаляет комментарий.
        /// </summary>
        /// <param name="request">Запрос удаления комментария.</param>        
        public async Task<DeleteCommentResponse> DeleteCommentAsync(DeleteCommentRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var article = await this.articleService.GetArticleByIdAsync(request.ArticleId);
            var comment = await this.commentService.GetCommentById(article, request.CommentId);

            await this.commentService.DeleteCommentAsync(article, comment);

            await this.unitOfWork.SaveChangesAsync();

            this.logger.Verbose("comment {CommentId} was deleted", request.CommentId);

            return new DeleteCommentResponse(true);
        }
    }
}
