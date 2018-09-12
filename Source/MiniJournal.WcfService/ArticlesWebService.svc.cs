﻿using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Application;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.Contracts.ImagesApplicationsService;
using Infotecs.MiniJournal.Contracts.UsersApplicationService;

namespace Infotecs.MiniJournal.WcfService
{
    /// <inheritdoc />
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ArticlesWebService : IArticlesWebService
    {
        private readonly IArticlesService articlesService;
        private readonly IImagesService imagesService;
        private readonly IUsersService usersService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesWebService"/> class.
        /// </summary>
        /// <param name="articlesService">Служба статей.</param>
        /// <param name="imagesService">Служба картинок.</param>
        /// <param name="usersService">Служба пользователей.</param>
        public ArticlesWebService(
            IArticlesService articlesService,
            IImagesService imagesService,
            IUsersService usersService)
        {
            this.usersService = usersService;
            this.articlesService = articlesService;
            this.imagesService = imagesService;
        }

        /// <inheritdoc />
        public Task<GetArticlesResponse> GetArticlesAsync(GetArticlesRequest request)
        {
            return this.articlesService.GetArticlesAsync(request);
        }

        /// <inheritdoc />
        public Task<CreateArticleResponse> CreateArticleAsync(CreateArticleRequest request)
        {
            return this.articlesService.CreateArticleAsync(request);
        }

        /// <inheritdoc />
        public Task<DeleteArticleResponse> DeleteArticleAsync(DeleteArticleRequest request)
        {
            return this.articlesService.DeleteArticleAsync(request);
        }

        /// <inheritdoc />
        public Task<AddCommentResponse> AddCommentAsync(AddCommentRequest request)
        {
            return this.articlesService.AddCommentAsync(request);
        }

        /// <inheritdoc />
        public Task<DeleteCommentResponse> DeleteCommentAsync(DeleteCommentRequest request)
        {
            return this.articlesService.DeleteCommentAsync(request);
        }

        /// <inheritdoc />
        public Task<FindImageResponse> FindImageAsync(FindImageRequest request)
        {
            return this.imagesService.FindImageAsync(request);
        }

        /// <inheritdoc />
        public Task<GetUserByNameResponse> GetUserByNameAsync(GetUserByNameRequest request)
        {
            return this.usersService.GetUserByNameAsync(request);
        }

        /// <inheritdoc />
        public Task<CreateNewUserResponse> CreateNewUserAsync(CreateNewUserRequest request)
        {
            return this.usersService.CreateNewUserAsync(request);
        }
    }
}
