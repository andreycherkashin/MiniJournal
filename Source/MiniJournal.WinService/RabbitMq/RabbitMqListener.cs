using System;
using System.Threading.Tasks;
using Autofac;
using Infotecs.MiniJournal.Application;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.Contracts.UsersApplicationService;
using RawRabbit;

namespace Infotecs.MiniJournal.WinService.RabbitMq
{
    /// <summary>
    /// Регистрирует слушателей очередей.
    /// </summary>
    public class RabbitMqListener
    {
        private readonly IBusClient busClient;
        private readonly ILifetimeScope lifetimeScope;

        /// <summary>
        /// Initializes a new instance of the <see cref="RabbitMqListener"/> class.
        /// </summary>
        /// <param name="busClient">Шина сообщений.</param>
        /// <param name="lifetimeScope">Контейнер.</param>
        public RabbitMqListener(
            IBusClient busClient,
            ILifetimeScope lifetimeScope)
        {
            this.busClient = busClient;
            this.lifetimeScope = lifetimeScope;
        }

        /// <summary>
        /// Зарегистрировать слушателей.
        /// </summary>
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

        /// <summary>
        /// Закрывает соединение с очередью.
        /// </summary>
        public void Stop()
        {
            this.busClient.ShutdownAsync(TimeSpan.FromSeconds(3)).Wait();
        }

        private async Task UsingAsync<T>(Func<T, Task> action)
        {
            using (ILifetimeScope scope = this.lifetimeScope.BeginLifetimeScope())
            {
                var service = scope.Resolve<T>();
                await action(service);
            }
        }

        private async Task<TResult> UsingAsync<T, TResult>(Func<T, Task<TResult>> action)
        {
            using (ILifetimeScope scope = this.lifetimeScope.BeginLifetimeScope())
            {
                var service = scope.Resolve<T>();
                return await action(service);
            }
        }
    }
}
