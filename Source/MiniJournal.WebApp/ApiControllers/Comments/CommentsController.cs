using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Application;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService.Entities;
using Infotecs.MiniJournal.Contracts.UsersApplicationService;
using Infotecs.MiniJournal.WebApp.ApiControllers.Comments.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Infotecs.MiniJournal.WebApp.ApiControllers.Comments
{
    /// <summary>
    /// Контроллер комментариев.
    /// </summary>
    [Route("api/Articles/{articleId:long}/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IArticlesService articlesService;
        private readonly IUsersService usersService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsController"/> class.
        /// </summary>
        /// <param name="articlesService"><see cref="IArticlesService"/>.</param>
        /// <param name="usersService"><see cref="IUsersService"/>.</param>
        public CommentsController(
            IArticlesService articlesService,
            IUsersService usersService)
        {
            this.articlesService = articlesService;
            this.usersService = usersService;
        }

        /// <summary>
        /// GET: api/Comments.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <returns>.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync(long articleId)
        {
            GetArticleResponse response = await this.articlesService.GetArticleAsync(new GetArticleRequest(articleId));
            List<Comment> comments = response?.Article?.Comments;

            if (comments == null)
            {
                return this.NotFound("article not found");
            }

            return this.Ok(comments);
        }


        /// <summary>
        /// GET: api/Comments/5.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <param name="id">Идентификатор комментария.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetAsync(long articleId, long id)
        {
            GetCommentResponse response = await this.articlesService.GetCommentAsync(new GetCommentRequest(id));
            Comment comment = response?.Comment;

            if (comment == null)
            {
                return this.NotFound("comment not found");
            }

            return this.Ok(comment);
        }


        /// <summary>
        /// POST: api/Comments.
        /// </summary>
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <param name="request"><see cref="AddComment"/>.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        public async Task<IActionResult> Post(long articleId, [FromBody] AddComment request)
        {
            GetUserByNameResponse userResponse = await this.usersService.GetUserByNameAsync(new GetUserByNameRequest(request.UserName));
            long userId = userResponse?.User?.Id ?? 0;

            if (userId == 0)
            {
                CreateNewUserResponse createUserResponse = await this.usersService.CreateNewUserAsync(new CreateNewUserRequest(request.UserName));
                userId = createUserResponse.UserId;
            }

            try
            {
                await this.articlesService.AddCommentAsync(new AddCommentRequest(userId, articleId, request.Text));
            }
            catch (Domain.Articles.Exceptions.ArticleNotFoundException)
            {
                return this.NotFound("article not found");
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
        /// <param name="articleId">Идентификатор статьи.</param>
        /// <param name="id">.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long articleId, int id)
        {
            try
            {
                await this.articlesService.DeleteCommentAsync(new DeleteCommentRequest(articleId, id));
            }
            catch (Domain.Articles.Exceptions.ArticleNotFoundException)
            {
                return this.NotFound("article not found");
            }
            catch (Domain.Comments.Exceptions.CommentNotFoundException)
            {
                return this.NotFound("comment not found");
            }

            return this.NoContent();
        }
    }
}
