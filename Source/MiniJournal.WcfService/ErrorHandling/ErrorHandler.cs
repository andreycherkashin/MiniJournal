using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace Infotecs.MiniJournal.WcfService.ErrorHandling
{
    public class ErrorHandler : IErrorHandler, IServiceBehavior
    {
        private readonly ILogger logger;

        public ErrorHandler(ILogger logger)
        {
            this.logger = logger;
        }

        public bool HandleError(Exception error)
        {
            this.logger.Error(error, "an error occured while processing request");
            return true;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
        }


        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {            
            foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                if (channelDispatcherBase is ChannelDispatcher channelDispatcher)
                {
                    channelDispatcher.ErrorHandlers.Add(this);
                }
            }
        }
    }
}