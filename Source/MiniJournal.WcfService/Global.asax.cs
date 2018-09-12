using System;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.Web;
using Autofac;
using Autofac.Integration.Wcf;
using StackExchange.Profiling;

namespace Infotecs.MiniJournal.WcfService
{
    /// <inheritdoc />
    public class Global : HttpApplication
    {
        /// <summary>
        /// Application_Start.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The evenArgs.</param>
        protected void Application_Start(object sender, EventArgs e)
        {
            // create io container
            var builder = new ContainerBuilder();
            builder.RegisterModule<WcfServiceModule>();
            IContainer container = builder.Build();

            // resolve and add to pipeline wcf service behaviors
            ResolveAndApplyServiceBehaviors(container);

            // set container
            AutofacHostFactory.Container = container;

            // configure profiler
            MiniProfiler.Configure(new MiniProfilerOptions
            {
                ResultsAuthorize = request => true
            });

            // set environment variable for serilog configuration
            Environment.SetEnvironmentVariable("BASEDIR", AppDomain.CurrentDomain.BaseDirectory);
        }

        /// <summary>
        /// Session_Start.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The evenArgs.</param>
        protected void Session_Start(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Application_BeginRequest.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The evenArgs.</param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (this.Request.IsLocal)
            {
                MiniProfiler.StartNew();
            }
        }

        /// <summary>
        /// Application_EndRequest.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The evenArgs.</param>
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            MiniProfiler.Current?.Stop();
        }

        /// <summary>
        /// Application_AuthenticateRequest.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The evenArgs.</param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Application_Error.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The evenArgs.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Session_End.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The evenArgs.</param>
        protected void Session_End(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Application_End.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The evenArgs.</param>
        protected void Application_End(object sender, EventArgs e)
        {
        }

        private static void ResolveAndApplyServiceBehaviors(IContainer container)
        {
            AutofacHostFactory.HostConfigurationAction += host =>
            {
                foreach (IServiceBehavior serviceBehavior in container.Resolve<IEnumerable<IServiceBehavior>>())
                {
                    host.Description.Behaviors.Add(serviceBehavior);
                }
            };
        }
    }
}
