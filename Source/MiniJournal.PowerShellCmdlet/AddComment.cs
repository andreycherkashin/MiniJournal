using System;
using System.Management.Automation;
using Autofac;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.RabbitMqClient;

namespace MiniJournal.PowerShellCmdlet
{
    [Cmdlet(VerbsCommon.Add, "Comment")]
    public class AddComment : PSCmdlet
    {
        private IContainer rootScope;

        [Parameter(Position = 0, Mandatory = true, HelpMessage = "Enter comment text")]
        [Alias("CommentText")]
        public string Text { get; set; }

        [Parameter(Position = 1, Mandatory = true, HelpMessage = "Enter commented article id")]
        [Alias("ArticleId")]
        public long Article { get; set; }

        [Parameter(Position = 2, Mandatory = true, HelpMessage = "Enter user name")]
        [Alias("UserName")]
        public string User { get; set; }

        [Parameter(Position = 4, Mandatory = false, HelpMessage = "Enter user name")]
        public string RabbitMqConnectionString { get; set; }

        protected override void BeginProcessing()
        {
            this.rootScope = Helpers.CreateContainer(this.RabbitMqConnectionString);
        }

        protected override void ProcessRecord()
        {
            using (var scope = this.rootScope.BeginLifetimeScope())
            {
                var client = scope.Resolve<IArticlesServiceRabbitMqClient>();
                var user = client.GetUser(this.User);
                client.AddCommentAsync(new AddCommentRequest(user.Id, this.Article, this.Text)).Wait();
            }
        }

        protected override void EndProcessing()
        {
            this.rootScope.Dispose();
        }
    }
}
