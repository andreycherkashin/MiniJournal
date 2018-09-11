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
            this.busClient.SubscribeAsync<AddCommentRequest>((request, context) => this.articlesService.AddCommentAsync(request));
            this.busClient.SubscribeAsync<DeleteCommentRequest>((request, context) => this.articlesService.DeleteCommentAsync(request));
            this.busClient.SubscribeAsync<CreateArticleRequest>((request, context) => this.articlesService.CreateArticleAsync(request));
            this.busClient.SubscribeAsync<DeleteArticleRequest>((request, context) => this.articlesService.DeleteArticleAsync(request));

            this.busClient.RespondAsync<GetUserByNameRequest, GetUserByNameResponse>((request, context) => this.usersService.GetUserByNameAsync(request));
            this.busClient.SubscribeAsync<CreateNewUserRequest>((request, context) => this.usersService.CreateNewUserAsync(request));            
        }

        public void Stop()
        {
            this.busClient.ShutdownAsync(TimeSpan.FromSeconds(3)).Wait();
        }
    }
}
