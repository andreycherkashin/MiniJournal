using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Infotecs.MiniJournal.WcfService.DataTransferObjects;
using MiniJournal.Application;
using Article = Infotecs.MiniJournal.WcfService.DataTransferObjects.Article;

namespace Infotecs.MiniJournal.WcfService
{    
    public class ArticlesWebService : IArticlesWebService
    {
        private readonly IUsersService usersService;
        private readonly IArticlesService articlesService;
        private readonly IMapper mapper;
        private readonly IImagesService imagesService;

        public ArticlesWebService(
            IArticlesService articlesService, 
            IMapper mapper,
            IImagesService imagesService,
            IUsersService usersService)
        {
            this.usersService = usersService;
            this.articlesService = articlesService;
            this.mapper = mapper;
            this.imagesService = imagesService;
        }

        public async Task<IEnumerable<Article>> GetArticlesAsync()
        {
            IEnumerable<Domain.Articles.Article> domainArticles = await this.articlesService.GetArticlesAsync();
            var dataTransferObjects = this.mapper.Map<IEnumerable<Article>>(domainArticles);
            return dataTransferObjects;
        }

        public Task CreateArticleAsync(string text, byte[] image, long userId)
        {
            return this.articlesService.CreateArticleAsync(text, image, userId);
        }

        public Task DeleteArticleAsync(long articleId)
        {
            return this.articlesService.DeleteArticleAsync(articleId);
        }

        public Task AddCommentAsync(string text, long userId, long articleId)
        {
            return this.articlesService.AddCommentAsync(text, userId, articleId);
        }

        public Task DeleteCommentAsync(long articleId, long commentId)
        {
            return this.articlesService.DeleteCommentAsync(articleId, commentId);
        }

        public Task<byte[]> FindImageAsync(string imageId)
        {
            return this.imagesService.FindImageAsync(imageId);
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            var user = await this.usersService.GetUserByNameAsync(name);
            return this.mapper.Map<User>(user);
        }

        public Task CreateNewUserAsync(string name)
        {
            return this.usersService.CreateNewUserAsync(name);
        }
    }
}
