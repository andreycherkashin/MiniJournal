using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Threading.Tasks;
using AutoMapper;
using Infotecs.MiniJournal.Application;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.Contracts.ImagesApplicationsService;
using Infotecs.MiniJournal.Contracts.UsersApplicationService;
using Infotecs.MiniJournal.Contracts.UsersApplicationService.Entities;
using Article = Infotecs.MiniJournal.Contracts.ArticlesApplicationService.Entites.Article;

namespace Infotecs.MiniJournal.WcfService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ArticlesWebService : IArticlesWebService
    {
        private readonly IUsersService usersService;
        private readonly IArticlesService articlesService;
        private readonly IImagesService imagesService;

        public ArticlesWebService(
            IArticlesService articlesService,
            IImagesService imagesService,
            IUsersService usersService)
        {
            this.usersService = usersService;
            this.articlesService = articlesService;
            this.imagesService = imagesService;
        }

        public Task<GetArticlesResponse> GetArticlesAsync(GetArticlesRequest request)
            => this.articlesService.GetArticlesAsync(request);

        /// <summary>
        /// Создать статью с указанным содержимым.
        /// </summary>
        public Task<CreateArticleResponse> CreateArticleAsync(CreateArticleRequest request)
            => this.articlesService.CreateArticleAsync(request);

        /// <summary>
        /// Удаляет статью.
        /// </summary>
        public Task<DeleteArticleResponse> DeleteArticleAsync(DeleteArticleRequest request)
            => this.articlesService.DeleteArticleAsync(request);

        /// <summary>
        /// Добавляет комментарий к статье.
        /// </summary>
        public Task<AddCommentResponse> AddCommentAsync(AddCommentRequest request)
            => this.articlesService.AddCommentAsync(request);

        /// <summary>
        /// Удаляет комментарий.
        /// </summary>
        public Task<DeleteCommentResponse> DeleteCommentAsync(DeleteCommentRequest request)
            => this.articlesService.DeleteCommentAsync(request);

        /// <summary>
        /// Находит картинку по идентификатору.
        /// </summary>
        public Task<FindImageResponse> FindImageAsync(FindImageRequest request)
            => this.imagesService.FindImageAsync(request);

        /// <summary>
        /// Получить пользователя по имени.
        /// <exception cref="Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException">
        /// Если пользователь с таким именем не найден будем выброшено исключение <see cref="Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException"/>. 
        /// </exception>
        /// </summary>        
        /// <returns>Найденный пользователь.</returns>
        public Task<GetUserByNameResponse> GetUserByNameAsync(GetUserByNameRequest request)
            => this.usersService.GetUserByNameAsync(request);

        /// <summary>
        /// Добавляет нового пользователя с указанным именем.
        /// </summary>
        public Task<CreateNewUserResponse> CreateNewUserAsync(CreateNewUserRequest request)
            => this.usersService.CreateNewUserAsync(request);
    }
}
