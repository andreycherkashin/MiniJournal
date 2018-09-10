using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Autofac;
using Autofac.Integration.Wcf;
using StackExchange.Profiling;

namespace Infotecs.MiniJournal.WcfService
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // create io container
            var builder = new ContainerBuilder();
            builder.RegisterModule<WcfServiceModule>();
            var container = builder.Build();

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

        private static void ResolveAndApplyServiceBehaviors(IContainer container)
        {
            AutofacHostFactory.HostConfigurationAction += host =>
            {
                foreach (var serviceBehavior in container.Resolve<IEnumerable<IServiceBehavior>>())
                {
                    host.Description.Behaviors.Add(serviceBehavior);
                }
            };
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (this.Request.IsLocal)
            {
                MiniProfiler.StartNew();
            }
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            MiniProfiler.Current?.Stop();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}