using System;
using System.Management.Automation;
using Autofac;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.RabbitMqClient;

namespace MiniJournal.PowerShellCmdlet
{
    [Cmdlet(VerbsCommon.Remove, "Article")]
    public class RemoveArticle : PSCmdlet
    {
        private IContainer rootScope;

        [Parameter(Position = 0, Mandatory = true, HelpMessage = "Enter commented article id")]
        [Alias("ArticleId")]
        public long Article { get; set; }

        [Parameter(Position = 1, Mandatory = false, HelpMessage = "Enter RabbitMq connection string")]
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
                client.DeleteArticleAsync(new DeleteArticleRequest(this.Article)).Wait();
            }
        }

        protected override void EndProcessing()
        {
            this.rootScope.Dispose();
        }
    }
}
