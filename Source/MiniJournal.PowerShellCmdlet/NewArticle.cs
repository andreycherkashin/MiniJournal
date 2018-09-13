using System;
using System.IO;
using System.Management.Automation;
using Autofac;
using Infotecs.MiniJournal.WcfServiceClient.ArticlesServiceReference;

namespace MiniJournal.PowerShellCmdlet
{
    /// <inheritdoc />
    [Cmdlet(VerbsCommon.New, "Article")]
    public class NewArticle : PSCmdlet
    {
        private ArticlesWebServiceClient client;

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
        [Parameter(Position = 4, Mandatory = false, HelpMessage = "Enter articles web service Url")]
        public string ServiceUrl { get; set; }

        /// <inheritdoc />
        protected override void BeginProcessing()
        {
            this.client = Helpers.CreateWcfServiceClient(this.ServiceUrl);
        }

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            byte[] image = null;
            if (!string.IsNullOrWhiteSpace(this.Image))
            {
                image = File.ReadAllBytes(this.Image);
            }
            
            var user = this.client.GetUser(this.User);
            this.client.CreateArticle(new CreateArticleRequest { UserId = user.Id, Text = this.Text, Image = image });
        }

        /// <inheritdoc />
        protected override void EndProcessing()
        {
            this.client.Close();
        }
    }
}
