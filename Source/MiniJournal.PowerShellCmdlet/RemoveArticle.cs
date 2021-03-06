﻿using System;
using System.Management.Automation;
using Autofac;
using Infotecs.MiniJournal.Contracts;
using Infotecs.MiniJournal.Contracts.ArticlesApplicationService;
using Infotecs.MiniJournal.Events;
using Infotecs.MiniJournal.Events.Commands;

namespace MiniJournal.PowerShellCmdlet
{
    /// <inheritdoc />
    [Cmdlet(VerbsCommon.Remove, "Article")]
    public class RemoveArticle : PSCmdlet
    {
        private IContainer rootScope;

        /// <summary>
        /// Идентификатор статьи.
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, HelpMessage = "Enter commented article id")]
        [Alias("ArticleId")]
        public long Article { get; set; }

        /// <summary>
        /// Строка подключения к брокеру сообщений RabbitMq.
        /// </summary>
        [Parameter(Position = 1, Mandatory = false, HelpMessage = "Enter RabbitMq connection string")]
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
                var commandDispatcher = scope.Resolve<ICommandDispatcher>();
                commandDispatcher.DispatchAsync(new DeleteArticleCommand(this.Article)).Wait();
            }
        }

        /// <inheritdoc />
        protected override void EndProcessing()
        {
            this.rootScope.Dispose();
        }
    }
}
