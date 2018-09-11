﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.Contracts.UsersApplicationService;
using RawRabbit;

namespace Infotecs.MiniJournal.RabbitMqClient
{
    public class ArticlesServiceRabbitMqClient : IArticlesServiceRabbitMqClient
    {
        private readonly IBusClient busClient;

        public ArticlesServiceRabbitMqClient(IBusClient busClient)
        {
            this.busClient = busClient;
        }

        /// <summary>
        /// Создать статью с указанным содержимым.
        /// </summary>
        /// <param name="request">Запрос создания статьи.</param>
        public Task<CreateArticleResponse> CreateArticleAsync(CreateArticleRequest request)
            => this.busClient.RequestAsync<CreateArticleRequest, CreateArticleResponse>(request);

        /// <summary>
        /// Удаляет статью.
        /// </summary>
        /// <param name="request">Запрос удаления статьи.</param>
        public Task<DeleteArticleResponse> DeleteArticleAsync(DeleteArticleRequest request)
            => this.busClient.RequestAsync<DeleteArticleRequest, DeleteArticleResponse>(request);

        /// <summary>
        /// Добавляет комментарий к статье.
        /// </summary>
        /// <param name="request">Запрос добавления статьи.</param>        
        public Task<AddCommentResponse> AddCommentAsync(AddCommentRequest request)
            => this.busClient.RequestAsync<AddCommentRequest, AddCommentResponse>(request);

        /// <summary>
        /// Удаляет комментарий.
        /// </summary>
        /// <param name="request">Запрос удаления комментария.</param>        
        public Task<DeleteCommentResponse> DeleteCommentAsync(DeleteCommentRequest request)
            => this.busClient.RequestAsync<DeleteCommentRequest, DeleteCommentResponse>(request);

        /// <summary>
        /// Получить пользователя по имени.
        /// <exception cref="Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException">
        /// Если пользователь с таким именем не найден будем выброшено исключение <see cref="Infotecs.MiniJournal.Domain.Users.Exceptions.UserNotFoundException"/>. 
        /// </exception>
        /// </summary>
        /// <param name="request">Имя пользователя.</param>
        /// <returns>Найденный пользователь.</returns>
        public Task<GetUserByNameResponse> GetUserByNameAsync(GetUserByNameRequest request)
            => this.busClient.RequestAsync<GetUserByNameRequest, GetUserByNameResponse>(request);

        /// <summary>
        /// Добавляет нового пользователя с указанным именем.
        /// </summary>
        /// <param name="request">Имя пользователя.</param>
        public Task<CreateNewUserResponse> CreateNewUserAsync(CreateNewUserRequest request)
            => this.busClient.RequestAsync<CreateNewUserRequest, CreateNewUserResponse>(request);
    }
}
