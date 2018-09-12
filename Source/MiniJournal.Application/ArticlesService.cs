using System;
using System.Collections.Generic;
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
    /// <inheritdoc />
    internal class ArticlesService : IArticlesService
    {
        private readonly IArticleFactory articleFactory;
        private readonly IArticleRepository articleRepository;
        private readonly IArticleDomainService articleService;
        private readonly ICommentFactory commentFactory;
        private readonly ICommentDomainService commentService;
        private readonly IImagesService imagesService;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserDomainService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesService"/> class.
        /// </summary>
        /// <param name="unitOfWork">Implementation of <see cref="IUnitOfWork"/>.</param>
        /// <param name="articleFactory">Implementation of <see cref="IArticleFactory"/>.</param>
        /// <param name="articleRepository">Implementation of <see cref="IArticleRepository"/>.</param>
        /// <param name="articleService">Implementation of <see cref="IArticleDomainService"/>.</param>
        /// <param name="commentFactory">Implementation of <see cref="ICommentFactory"/>.</param>
        /// <param name="commentService">Implementation of <see cref="ICommentDomainService"/>.</param>
        /// <param name="userService">Implementation of <see cref="IUserDomainService"/>.</param>
        /// <param name="imagesService">Implementation of <see cref="IImagesService"/>.</param>
        /// <param name="logger">Implementation of <see cref="ILogger"/>.</param>
        /// <param name="mapper">Implementation of <see cref="IMapper"/>.</param>
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

        /// <inheritdoc />
        public async Task<GetArticlesResponse> GetArticlesAsync(GetArticlesRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            IEnumerable<Article> articles = await this.articleRepository.GetArticlesAsync();

            return new GetArticlesResponse
            {
                Articles = this.mapper.Map<List<Contracts.ArticlesApplicationService.Entities.Article>>(articles)
            };
        }

        /// <inheritdoc />
        public async Task<CreateArticleResponse> CreateArticleAsync(CreateArticleRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            User user = await this.userService.GetUserByIdAsync(request.UserId);
            UploadImageResponse createImageResponse = await this.imagesService.UploadImageAsync(new UploadImageRequest { Image = request.Image });
            Article article = await this.articleFactory.CreateAsync(request.Text, createImageResponse.ImageId, user);

            await this.articleService.CreateArticleAsync(article);

            await this.unitOfWork.SaveChangesAsync();

            this.logger.Verbose("user {UserId} created new article: {ArticleText}", request.UserId, article.Text.Truncate(30, true));

            return new CreateArticleResponse(true);
        }

        /// <inheritdoc />
        public async Task<DeleteArticleResponse> DeleteArticleAsync(DeleteArticleRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            Article article = await this.articleService.GetArticleByIdAsync(request.ArticleId);
            await this.articleService.DeleteArticleAsync(article);

            await this.unitOfWork.SaveChangesAsync();

            this.logger.Verbose("article {ArticleId} was deleted", request.ArticleId);

            return new DeleteArticleResponse(true);
        }

        /// <inheritdoc />
        public async Task<AddCommentResponse> AddCommentAsync(AddCommentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            User user = await this.userService.GetUserByIdAsync(request.UserId);
            Article article = await this.articleService.GetArticleByIdAsync(request.ArticleId);
            Comment comment = await this.commentFactory.CreateAsync(request.Text, user, article);

            await this.commentService.AddCommentAsync(article, comment);

            await this.unitOfWork.SaveChangesAsync();

            this.logger.Verbose("new comment added to article {ArticleId}, comment text: {CommentText}", request.ArticleId, comment.Text.Truncate(30, true));

            return new AddCommentResponse(true);
        }

        /// <inheritdoc />
        public async Task<DeleteCommentResponse> DeleteCommentAsync(DeleteCommentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            Article article = await this.articleService.GetArticleByIdAsync(request.ArticleId);
            Comment comment = await this.commentService.GetCommentById(article, request.CommentId);

            await this.commentService.DeleteCommentAsync(article, comment);

            await this.unitOfWork.SaveChangesAsync();

            this.logger.Verbose("comment {CommentId} was deleted", request.CommentId);

            return new DeleteCommentResponse(true);
        }
    }
}
