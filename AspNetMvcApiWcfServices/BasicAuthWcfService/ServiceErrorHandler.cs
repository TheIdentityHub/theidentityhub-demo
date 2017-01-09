using System;
using System.Collections.ObjectModel;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace BasicAuthWcfService
{
    public class ServiceErrorBehaviour : IServiceBehavior
    {
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        { }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            var handler = new ServiceErrorHandler();
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                dispatcher.ErrorHandlers.Add(handler);
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        { }
    }

    public class ServiceErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            return error is UnauthorizedAccessException;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error is UnauthorizedAccessException)
            {
                fault = Message.CreateMessage(version, null);
                var prop = new HttpResponseMessageProperty();
                prop.StatusCode = HttpStatusCode.Unauthorized;
                prop.Headers[HttpResponseHeader.WwwAuthenticate] = "Basic";
                fault.Properties.Add(HttpResponseMessageProperty.Name, prop);
            }
        }
    }

    public class ServiceErrorHandlerBehaviour : BehaviorExtensionElement
    {
        public override Type BehaviorType { get { return typeof(ServiceErrorBehaviour); } }

        protected override object CreateBehavior()
        {
            return new ServiceErrorBehaviour();
        }
    }
}