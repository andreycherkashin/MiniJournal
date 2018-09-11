using RawRabbit;
using System;
using Infotecs.MiniJournal.Application;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.Contracts.ImagesApplicationsService;
using Infotecs.MiniJournal.Contracts.UsersApplicationService;

namespace Infotecs.MiniJournal.WinService.RabbitMq
{
    public class RabbitMqListener
    {
        private readonly IBusClient busClient;
        private readonly IArticlesService articlesService;
        private readonly IUsersService usersService;

        public RabbitMqListener(
            IBusClient busClient,
            IArticlesService articlesService,
            IUsersService usersService)
        {
            this.busClient = busClient;
            this.articlesService = articlesService;
            this.usersService = usersService;
        }

        public void Start()
        {
            this.busClient.RespondAsync<AddCommentRequest, AddCommentResponse>((request, context) => this.articlesService.AddCommentAsync(request));
            this.busClient.RespondAsync<DeleteCommentRequest, DeleteCommentResponse>((request, context) => this.articlesService.DeleteCommentAsync(request));
            this.busClient.RespondAsync<CreateArticleRequest, CreateArticleResponse>((request, context) => this.articlesService.CreateArticleAsync(request));
            this.busClient.RespondAsync<DeleteArticleRequest, DeleteArticleResponse>((request, context) => this.articlesService.DeleteArticleAsync(request));

            this.busClient.RespondAsync<GetUserByNameRequest, GetUserByNameResponse>((request, context) => this.usersService.GetUserByNameAsync(request));
            this.busClient.RespondAsync<CreateNewUserRequest, CreateNewUserResponse>((request, context) => this.usersService.CreateNewUserAsync(request));            
        }

        public void Stop()
        {
            this.busClient.ShutdownAsync(TimeSpan.FromSeconds(3)).Wait();
        }
    }
}
