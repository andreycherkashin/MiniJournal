using RawRabbit;
using System;
using System.Threading.Tasks;
using Autofac;
using Infotecs.MiniJournal.Application;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.Contracts.ImagesApplicationsService;
using Infotecs.MiniJournal.Contracts.UsersApplicationService;

namespace Infotecs.MiniJournal.WinService.RabbitMq
{
    public class RabbitMqListener
    {
        private readonly IBusClient busClient;        
        private readonly ILifetimeScope lifetimeScope;

        public RabbitMqListener(
            IBusClient busClient,            
            ILifetimeScope lifetimeScope)
        {
            this.busClient = busClient;            
            this.lifetimeScope = lifetimeScope;
        }

        public void Start()
        {
            this.busClient.SubscribeAsync<AddCommentRequest>((request, context) 
                => this.UsingAsync<IArticlesService>(service => service.AddCommentAsync(request)));

            this.busClient.SubscribeAsync<DeleteCommentRequest>((request, context) 
                => this.UsingAsync<IArticlesService>(service => service.DeleteCommentAsync(request)));

            this.busClient.SubscribeAsync<CreateArticleRequest>((request, context) 
                => this.UsingAsync<IArticlesService>(service => service.CreateArticleAsync(request)));

            this.busClient.SubscribeAsync<DeleteArticleRequest>((request, context)
                => this.UsingAsync<IArticlesService>(service => service.DeleteArticleAsync(request)));


            this.busClient.RespondAsync<GetUserByNameRequest, GetUserByNameResponse>((request, context)
                => this.UsingAsync<IUsersService, GetUserByNameResponse>(service => service.GetUserByNameAsync(request)));

            this.busClient.SubscribeAsync<CreateNewUserRequest>((request, context)
                => this.UsingAsync<IUsersService>(service => service.CreateNewUserAsync(request)));            
        }

        public void Stop()
        {
            this.busClient.ShutdownAsync(TimeSpan.FromSeconds(3)).Wait();
        }

        private async Task UsingAsync<T>(Func<T, Task> action)
        {
            using (var scope = this.lifetimeScope.BeginLifetimeScope())
            {
                var service = scope.Resolve<T>();
                await action(service);
            }
        }

        private async Task<TResult> UsingAsync<T, TResult>(Func<T, Task<TResult>> action)
        {
            using (var scope = this.lifetimeScope.BeginLifetimeScope())
            {
                var service = scope.Resolve<T>();
                return await action(service);
            }
        }
    }
}
