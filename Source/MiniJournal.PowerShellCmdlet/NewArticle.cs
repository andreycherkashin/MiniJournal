using System;
using System.IO;
using System.Management.Automation;
using Autofac;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.Contracts.UsersApplicationService.Entities;
using Infotecs.MiniJournal.RabbitMqClient;

namespace MiniJournal.PowerShellCmdlet
{
    /// <inheritdoc />
    [Cmdlet(VerbsCommon.New, "Article")]
    public class NewArticle : PSCmdlet
    {
        private IContainer rootScope;

        /// <summary>
        /// Текст статьи.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, HelpMessage = "Enter article text")]
        [Alias("ArticleText")]
        public string Text { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Parameter(Position = 2, Mandatory = true, HelpMessage = "Enter user name")]
        [Alias("UserName")]
        public string User { get; set; }

        /// <summary>
        /// Путь к картинке.
        /// </summary>
        [Parameter(Position = 3, Mandatory = false, HelpMessage = "Path to article image")]
        [Alias("ImagePath")]
        public string Image { get; set; }

        /// <summary>
        /// Строка подключения к брокеру сообщений RabbitMq.
        /// </summary>
        [Parameter(Position = 4, Mandatory = false, HelpMessage = "Enter RabbitMq connection string")]
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
                byte[] image = null;
                if (!string.IsNullOrWhiteSpace(this.Image))
                {
                    image = File.ReadAllBytes(this.Image);
                }

                var client = scope.Resolve<IArticlesServiceRabbitMqClient>();
                User user = client.GetUser(this.User);
                client.CreateArticleAsync(new CreateArticleRequest(this.Text, image, user.Id)).Wait();
            }
        }

        /// <inheritdoc />
        protected override void EndProcessing()
        {
            this.rootScope.Dispose();
        }
    }
}
