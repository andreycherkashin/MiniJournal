using System;
using System.IO;
using System.Management.Automation;
using Autofac;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.RabbitMqClient;

namespace MiniJournal.PowerShellCmdlet
{
    [Cmdlet(VerbsCommon.New, "Article")]
    public class NewArticle : PSCmdlet
    {
        private IContainer rootScope;

        [Parameter(Position = 0, Mandatory = true, HelpMessage = "Enter article text")]
        [Alias("ArticleText")]
        public string Text { get; set; }        

        [Parameter(Position = 2, Mandatory = true, HelpMessage = "Enter user name")]
        [Alias("UserName")]
        public string User { get; set; }

        [Parameter(Position = 3, Mandatory = false, HelpMessage = "Path to article image")]
        [Alias("ImagePath")]
        public string Image { get; set; }

        [Parameter(Position = 4, Mandatory = false, HelpMessage = "Enter RabbitMq connection string")]
        public string RabbitMqConnectionString { get; set; }

        protected override void BeginProcessing()
        {
            this.rootScope = Helpers.CreateContainer(this.RabbitMqConnectionString);
        }

        protected override void ProcessRecord()
        {
            using (var scope = this.rootScope.BeginLifetimeScope())
            {
                byte[] image = null;
                if (!string.IsNullOrWhiteSpace(this.Image))
                {
                    image = File.ReadAllBytes(this.Image);
                }

                var client = scope.Resolve<IArticlesServiceRabbitMqClient>();
                var user = client.GetUser(this.User);
                client.CreateArticleAsync(new CreateArticleRequest(this.Text, image, user.Id)).Wait();
            }
        }

        protected override void EndProcessing()
        {
            this.rootScope.Dispose();
        }
    }
}
