using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Serilog;

namespace Infotecs.MiniJournal.WcfService.ErrorHandling
{
    /// <inheritdoc cref="IErrorHandler"/>
    public class ErrorHandler : IErrorHandler, IServiceBehavior
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandler"/> class.
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/>.</param>
        public ErrorHandler(ILogger logger)
        {
            this.logger = logger;
        }

        /// <inheritdoc />
        public bool HandleError(Exception error)
        {
            this.logger.Error(error, "[HandleError] an error occured while processing request");
            return true;
        }

        /// <inheritdoc />
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            this.logger.Error(error, "[ProvideFault] an error occured while processing request");
        }
        
        /// <inheritdoc />
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        /// <inheritdoc />
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        /// <inheritdoc />
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
