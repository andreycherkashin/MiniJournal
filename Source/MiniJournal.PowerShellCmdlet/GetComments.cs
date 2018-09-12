using System;
using System.Linq;
using System.Management.Automation;
using Infotecs.MiniJournal.WcfServiceClient.ArticlesServiceReference;

namespace MiniJournal.PowerShellCmdlet
{
    /// <inheritdoc />
    [Cmdlet(VerbsCommon.Get, "Comments")]
    public class GetComments : PSCmdlet
    {
        private ArticlesWebServiceClient client;

        /// <summary>
        /// Идентификатор статьи.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, HelpMessage = "Enter commented article id")]
        [Alias("ArticleId")]
        public long Article { get; set; }

        /// <summary>
        /// Адрес веб службы.
        /// </summary>
        [Parameter(Position = 1, Mandatory = false, HelpMessage = "Enter articles web service Url")]
        public string ServiceUrl { get; set; }

        /// <inheritdoc />
        protected override void BeginProcessing()
        {
            this.client = Helpers.CreateWcfServiceClient(this.ServiceUrl);
        }

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            Article article = this.client.GetArticles(new GetArticlesRequest()).Articles.FirstOrDefault(x => x.Id == this.Article);
            this.WriteObject(
                article.Comments.Select(x => new
                {
                    x.Id,
                    UserName = x.User.Name,
                    x.Text
                }),
                true);
        }

        /// <inheritdoc />
        protected override void EndProcessing()
        {
            this.client.Close();
        }
    }
}
