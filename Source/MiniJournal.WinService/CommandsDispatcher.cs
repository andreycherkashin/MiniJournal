using System;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Infotecs.MiniJournal.Application;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.Contracts.UsersApplicationService;
using Infotecs.MiniJournal.Events;
using Infotecs.MiniJournal.Events.Commands;

namespace Infotecs.MiniJournal.WinService
{
    /// <summary>
    /// Передает команды модулю ApplicationServices.
    /// </summary>
    public class CommandsDispatcher
    {
        private readonly IMessageBus messageBus;
        private readonly ILifetimeScope lifetimeScope;
        private MapperConfiguration commandMapperConfiguration;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandsDispatcher"/> class.
        /// </summary>
        /// <param name="messageBus"><see cref="IMessageBus"/>.</param>
        /// <param name="lifetimeScope"><see cref="ILifetimeScope"/>.</param>
        public CommandsDispatcher(
            IMessageBus messageBus,
            ILifetimeScope lifetimeScope)
        {
            this.messageBus = messageBus;
            this.lifetimeScope = lifetimeScope;
        }

        /// <summary>
        /// Регистрирует обработчики команд.
        /// </summary>
        public void Start()
        {
            this.messageBus.SubscribeToCommand<AddCommentCommand>(command
                => this.UsingAsync<IArticlesService>(service => service.AddCommentAsync(this.Map<AddCommentRequest>(command))));

            this.messageBus.SubscribeToCommand<DeleteCommentCommand>(command
                => this.UsingAsync<IArticlesService>(service => service.DeleteCommentAsync(this.Map<DeleteCommentRequest>(command))));

            this.messageBus.SubscribeToCommand<CreateArticleCommand>(command
                => this.UsingAsync<IArticlesService>(service => service.CreateArticleAsync(this.Map<CreateArticleRequest>(command))));

            this.messageBus.SubscribeToCommand<DeleteArticleCommand>(command
                => this.UsingAsync<IArticlesService>(service => service.DeleteArticleAsync(this.Map<DeleteArticleRequest>(command))));

            this.messageBus.SubscribeToCommand<CreateNewUserCommand>(command
                => this.UsingAsync<IUsersService>(service => service.CreateNewUserAsync(this.Map<CreateNewUserRequest>(command))));
        }

        /// <summary>
        /// Закрывает соединение с шиной.
        /// </summary>
        public void Stop()
        {
            this.messageBus.Dispose();
            this.lifetimeScope.Dispose();
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

        private TRequest Map<TRequest>(ICommand command) 
            => this.GetMapper().Map<TRequest>(command);

        private IMapper GetMapper()
        {
            if (this.commandMapperConfiguration == null)
            {
                this.commandMapperConfiguration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AddCommentCommand, AddCommentRequest>();
                    cfg.CreateMap<DeleteCommentCommand, DeleteCommentRequest>();
                    cfg.CreateMap<CreateArticleCommand, CreateArticleRequest>();
                    cfg.CreateMap<DeleteArticleCommand, DeleteArticleRequest>();
                    cfg.CreateMap<CreateNewUserCommand, CreateNewUserRequest>();
                });

                this.commandMapperConfiguration.AssertConfigurationIsValid();
            }

            return this.commandMapperConfiguration.CreateMapper();
        }
    }
}
