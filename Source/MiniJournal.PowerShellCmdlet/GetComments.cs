using System;
using System.Linq;
using System.Management.Automation;
using System.ServiceModel;
using Infotecs.MiniJournal.WcfServiceClient.ArticlesServiceReference;

namespace MiniJournal.PowerShellCmdlet
{
    [Cmdlet(VerbsCommon.Get, "Comments")]
    public class GetComments : PSCmdlet
    {
        private ArticlesWebServiceClient client;

        [Parameter(Position = 0, Mandatory = true, HelpMessage = "Enter commented article id")]
        [Alias("ArticleId")]
        public long Article { get; set; }

        [Parameter(Position = 1, Mandatory = false, HelpMessage = "Enter articles web service Url")]
        public string ServiceUrl { get; set; }

        protected override void BeginProcessing()
        {
            this.client = Helpers.CreateWcfServiceClient(this.ServiceUrl);
        }

        protected override void ProcessRecord()
        {
            var article = this.client.GetArticles(new GetArticlesRequest()).Articles.FirstOrDefault(x => x.Id == this.Article);
            this.WriteObject(
                article.Comments.Select(x => new
                {
                    x.Id,
                    UserName = x.User.Name,
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
