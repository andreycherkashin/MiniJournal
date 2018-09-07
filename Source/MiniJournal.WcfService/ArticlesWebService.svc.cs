using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MiniJournal.Application;
using Article = Infotecs.MiniJournal.WcfService.DataTransferObjects.Article;

namespace Infotecs.MiniJournal.WcfService
{    
    public class ArticlesWebService : IArticlesWebService
    {
        private readonly IArticlesService articlesService;
        private readonly IMapper mapper;        

        public ArticlesWebService(IArticlesService articlesService, IMapper mapper)
        {
            this.articlesService = articlesService;
            this.mapper = mapper;
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
    }
}
