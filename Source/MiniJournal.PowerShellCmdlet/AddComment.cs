using System;
using System.Management.Automation;
using Autofac;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.Contracts.UsersApplicationService.Entities;
using Infotecs.MiniJournal.RabbitMqClient;

namespace MiniJournal.PowerShellCmdlet
{
    /// <inheritdoc />
    [Cmdlet(VerbsCommon.Add, "Comment")]
    public class AddComment : PSCmdlet
    {
        private IContainer rootScope;

        /// <summary>
        /// Текст комментария.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, HelpMessage = "Enter comment text")]
        [Alias("CommentText")]
        public string Text { get; set; }

        /// <summary>
        /// Идентификатор статьи.
        /// </summary>
        [Parameter(Position = 1, Mandatory = true, HelpMessage = "Enter commented article id")]
        [Alias("ArticleId")]
        public long Article { get; set; }

        /// <summary>
        /// Имя пользователь.
        /// </summary>
        [Parameter(Position = 2, Mandatory = true, HelpMessage = "Enter user name")]
        [Alias("UserName")]
        public string User { get; set; }

        /// <summary>
        /// Строка подключения к брокеру сообщений RabbitMq.
        /// </summary>
        [Parameter(Position = 4, Mandatory = false, HelpMessage = "Enter user name")]
        public string RabbitMqConnectionString { get; set; }

        /// <inheritdoc />
        protected override void BeginProcessing()
        {
            this.rootScope = Helpers.CreateContainer(this.RabbitMqConnectionString);
        }

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            using (ILifetimeScope scope = this.rootScope.BeginLifetimeScope())
            {
                var client = scope.Resolve<IArticlesServiceRabbitMqClient>();
                User user = client.GetUser(this.User);
                client.AddCommentAsync(new AddCommentRequest(user.Id, this.Article, this.Text)).Wait();
            }
        }

        /// <inheritdoc />
        protected override void EndProcessing()
        {
            this.rootScope.Dispose();
        }
    }
}
