using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Autofac;
using Autofac.Integration.Wcf;

namespace Infotecs.MiniJournal.WcfService
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<WcfServiceModule>();

            var container = builder.Build();

            ResolveAndApplyServiceBehaviors(container);

            AutofacHostFactory.Container = container;
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