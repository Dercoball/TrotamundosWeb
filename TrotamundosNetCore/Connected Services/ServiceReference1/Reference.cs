﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Runtime.Serialization;

namespace ServiceReference1
{
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "StatusEnvio", Namespace = "http://tempuri.org/")]
    public partial class StatusEnvio : object
    {

        private string StatusField;

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
        public string Status
        {
            get
            {
                return this.StatusField;
            }
            set
            {
                this.StatusField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "ServiceReference1.Service1Soap")]
    public interface Service1Soap
    {

        // CODEGEN: Generating message contract since element name mensaje from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/EnviarCorreo", ReplyAction = "*")]
        ServiceReference1.EnviarCorreoResponse EnviarCorreo(ServiceReference1.EnviarCorreoRequest request);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/EnviarCorreo", ReplyAction = "*")]
        System.Threading.Tasks.Task<ServiceReference1.EnviarCorreoResponse> EnviarCorreoAsync(ServiceReference1.EnviarCorreoRequest request);
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class EnviarCorreoRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "EnviarCorreo", Namespace = "http://tempuri.org/", Order = 0)]
        public ServiceReference1.EnviarCorreoRequestBody Body;

        public EnviarCorreoRequest()
        {
        }

        public EnviarCorreoRequest(ServiceReference1.EnviarCorreoRequestBody Body)
        {
            this.Body = Body;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace = "http://tempuri.org/")]
    public partial class EnviarCorreoRequestBody
    {

        [System.Runtime.Serialization.DataMemberAttribute(Order = 0)]
        public int sistema;

        [System.Runtime.Serialization.DataMemberAttribute(Order = 1)]
        public int subsistema;

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 2)]
        public string mensaje;

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 3)]
        public string Titulo;

        [System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public int tipo;

        public EnviarCorreoRequestBody()
        {
        }

        public EnviarCorreoRequestBody(int sistema, int subsistema, string mensaje, string Titulo, int tipo)
        {
            this.sistema = sistema;
            this.subsistema = subsistema;
            this.mensaje = mensaje;
            this.Titulo = Titulo;
            this.tipo = tipo;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class EnviarCorreoResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "EnviarCorreoResponse", Namespace = "http://tempuri.org/", Order = 0)]
        public ServiceReference1.EnviarCorreoResponseBody Body;

        public EnviarCorreoResponse()
        {
        }

        public EnviarCorreoResponse(ServiceReference1.EnviarCorreoResponseBody Body)
        {
            this.Body = Body;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace = "http://tempuri.org/")]
    public partial class EnviarCorreoResponseBody
    {

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 0)]
        public ServiceReference1.StatusEnvio[] EnviarCorreoResult;

        public EnviarCorreoResponseBody()
        {
        }

        public EnviarCorreoResponseBody(ServiceReference1.StatusEnvio[] EnviarCorreoResult)
        {
            this.EnviarCorreoResult = EnviarCorreoResult;
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public interface Service1SoapChannel : ServiceReference1.Service1Soap, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public partial class Service1SoapClient : System.ServiceModel.ClientBase<ServiceReference1.Service1Soap>, ServiceReference1.Service1Soap
    {

        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);

        public Service1SoapClient(EndpointConfiguration endpointConfiguration) :
                base(Service1SoapClient.GetBindingForEndpoint(endpointConfiguration), Service1SoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public Service1SoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) :
                base(Service1SoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public Service1SoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) :
                base(Service1SoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }

        public Service1SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ServiceReference1.EnviarCorreoResponse ServiceReference1.Service1Soap.EnviarCorreo(ServiceReference1.EnviarCorreoRequest request)
        {
            return base.Channel.EnviarCorreo(request);
        }

        public ServiceReference1.StatusEnvio[] EnviarCorreo(int sistema, int subsistema, string mensaje, string Titulo, int tipo)
        {
            ServiceReference1.EnviarCorreoRequest inValue = new ServiceReference1.EnviarCorreoRequest();
            inValue.Body = new ServiceReference1.EnviarCorreoRequestBody();
            inValue.Body.sistema = sistema;
            inValue.Body.subsistema = subsistema;
            inValue.Body.mensaje = mensaje;
            inValue.Body.Titulo = Titulo;
            inValue.Body.tipo = tipo;
            ServiceReference1.EnviarCorreoResponse retVal = ((ServiceReference1.Service1Soap)(this)).EnviarCorreo(inValue);
            return retVal.Body.EnviarCorreoResult;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ServiceReference1.EnviarCorreoResponse> ServiceReference1.Service1Soap.EnviarCorreoAsync(ServiceReference1.EnviarCorreoRequest request)
        {
            return base.Channel.EnviarCorreoAsync(request);
        }

        public System.Threading.Tasks.Task<ServiceReference1.EnviarCorreoResponse> EnviarCorreoAsync(int sistema, int subsistema, string mensaje, string Titulo, int tipo)
        {
            ServiceReference1.EnviarCorreoRequest inValue = new ServiceReference1.EnviarCorreoRequest();
            inValue.Body = new ServiceReference1.EnviarCorreoRequestBody();
            inValue.Body.sistema = sistema;
            inValue.Body.subsistema = subsistema;
            inValue.Body.mensaje = mensaje;
            inValue.Body.Titulo = Titulo;
            inValue.Body.tipo = tipo;
            return ((ServiceReference1.Service1Soap)(this)).EnviarCorreoAsync(inValue);
        }

        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }

        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }

        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.Service1Soap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.Service1Soap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }

        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.Service1Soap))
            {
                return new System.ServiceModel.EndpointAddress("http://10.53.28.77/ServicioCorreos/Service1.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.Service1Soap12))
            {
                return new System.ServiceModel.EndpointAddress("http://10.53.28.77/ServicioCorreos/Service1.asmx");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }

        public enum EndpointConfiguration
        {

            Service1Soap,

            Service1Soap12,
        }
    }
}
