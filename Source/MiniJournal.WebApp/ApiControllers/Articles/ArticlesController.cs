using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Application;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService.Entities;
using Infotecs.MiniJournal.Contracts.UsersApplicationService;
using Infotecs.MiniJournal.WebApp.ApiControllers.Articles.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Infotecs.MiniJournal.WebApp.ApiControllers.Articles
{
    /// <summary>
    /// Контроллер для статей.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesService articlesService;
        private readonly IUsersService usersService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesController"/> class.
        /// </summary>
        /// <param name="articlesService"><see cref="IArticlesService"/>.</param>
        /// <param name="usersService"><see cref="IUsersService"/>.</param>
        public ArticlesController(
            IArticlesService articlesService,
            IUsersService usersService)
        {
            this.articlesService = articlesService;
            this.usersService = usersService;
        }

        /// <summary>
        /// GET: api/Articles.
        /// </summary>
        /// <returns>.</returns>
        [HttpGet]
        public async Task<IEnumerable<Article>> GetAsync()
        {
            GetArticlesResponse response = await this.articlesService.GetArticlesAsync(new GetArticlesRequest());
            return response.Articles;
        }

        /// <summary>
        /// GET: api/Articles/5.
        /// </summary>
        /// <param name="id">.</param>
        /// <returns>.</returns>
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            GetArticleResponse response = await this.articlesService.GetArticleAsync(new GetArticleRequest(id));
            Article article = response?.Article;

            if (article == null)
            {
                return this.NotFound("article not found");
            }

            return this.Ok(article);
        }


        /// <summary>
        /// POST: api/Articles.
        /// </summary>
        /// <param name="request">.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateArticle request)
        {
            
            long userId;
            try
            {
                GetUserByNameResponse userResponse = await this.usersService.GetUserByNameAsync(new GetUserByNameRequest(request.UserName));
                userId = userResponse.User.Id;
            }
            catch (Domain.Users.Exceptions.UserNotFoundException)
            {
                CreateNewUserResponse createUserResponse = await this.usersService.CreateNewUserAsync(new CreateNewUserRequest(request.UserName));
                userId = createUserResponse.UserId;
            }            

            try
            {
                await this.articlesService.CreateArticleAsync(new CreateArticleRequest(request.Text, null, userId));
            }
            catch (Domain.Users.Exceptions.UserNotFoundException)
            {
                return this.NotFound("user not found");
            }

            return this.NoContent();
        }

        /// <summary>
        /// DELETE: api/ApiWithActions/5.
        /// </summary>
        /// <param name="id">.</param>
        /// <returns><see cref="IActionResult"/>.</returns>
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            try
            {
                await this.articlesService.DeleteArticleAsync(new DeleteArticleRequest(id));
            }
            catch (Domain.Articles.Exceptions.ArticleNotFoundException)
            {
                return this.NotFound("article not found");
            }

            return this.NoContent();
        }
    }
}
