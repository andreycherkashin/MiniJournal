using System;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.Contracts.UsersApplicationService;
using RawRabbit;

namespace Infotecs.MiniJournal.RabbitMqClient
{
    /// <inheritdoc />
    public class ArticlesServiceRabbitMqClient : IArticlesServiceRabbitMqClient
    {
        private readonly IBusClient busClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesServiceRabbitMqClient"/> class.
        /// </summary>
        /// <param name="busClient"><see cref="IBusClient"/>.</param>
        public ArticlesServiceRabbitMqClient(IBusClient busClient)
        {
            this.busClient = busClient;
        }

        /// <inheritdoc />
        public Task CreateArticleAsync(CreateArticleRequest request)
            => this.busClient.PublishAsync(request);

        /// <inheritdoc />
        public Task DeleteArticleAsync(DeleteArticleRequest request)
            => this.busClient.PublishAsync(request);

        /// <inheritdoc />
        public Task AddCommentAsync(AddCommentRequest request)
            => this.busClient.PublishAsync(request);

        /// <inheritdoc />
        public Task DeleteCommentAsync(DeleteCommentRequest request)
            => this.busClient.PublishAsync(request);

        /// <inheritdoc />
        public Task<GetUserByNameResponse> GetUserByNameAsync(GetUserByNameRequest request)
            => this.busClient.RequestAsync<GetUserByNameRequest, GetUserByNameResponse>(request);

        /// <inheritdoc />
        public Task CreateNewUserAsync(CreateNewUserRequest request)
            => this.busClient.PublishAsync(request);
    }
}
