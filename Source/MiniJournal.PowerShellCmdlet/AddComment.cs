using System;
using System.Management.Automation;
using Autofac;
using Infotecs.MiniJournal.WcfServiceClient.ArticlesServiceReference;

namespace MiniJournal.PowerShellCmdlet
{
    /// <inheritdoc />
    [Cmdlet(VerbsCommon.Add, "Comment")]
    public class AddComment : PSCmdlet
    {
        private ArticlesWebServiceClient client;

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
        [Parameter(Position = 3, Mandatory = false, HelpMessage = "Enter articles web service Url")]
        public string ServiceUrl { get; set; }

        /// <inheritdoc />
        protected override void BeginProcessing()
        {
            this.client = Helpers.CreateWcfServiceClient(this.ServiceUrl);
        }

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            var user = this.client.GetUser(this.User);
            this.client.AddComment(new AddCommentRequest { ArticleId = this.Article, UserId = user.Id, Text = this.Text});
        }

        /// <inheritdoc />
        protected override void EndProcessing()
        {
            this.client.Close();
        }
    }
}
