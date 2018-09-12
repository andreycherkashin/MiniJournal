using System;
using System.Linq;
using System.Management.Automation;
using Infotecs.MiniJournal.WcfServiceClient.ArticlesServiceReference;

namespace MiniJournal.PowerShellCmdlet
{
    [Cmdlet(VerbsCommon.Get, "Articles")]
    public class GetArticles : PSCmdlet
    {
        private ArticlesWebServiceClient client;

        [Parameter(Position = 0, Mandatory = false, HelpMessage = "Enter articles web service Url")]
        public string ServiceUrl { get; set; }

        protected override void BeginProcessing()
        {
            this.client = Helpers.CreateWcfServiceClient(this.ServiceUrl);
        }

        protected override void ProcessRecord()
        {
            Article[] articles = this.client.GetArticles(new GetArticlesRequest()).Articles;
            this.WriteObject(
                articles.Select(x => new
                {
                    x.Id,
                    UserName = x.User.Name,
                    CommentsCount = x.Comments.Length,
                    x.Text
                }),
                true);
        }

        protected override void EndProcessing()
        {
            this.client.Close();
        }
    }
}
