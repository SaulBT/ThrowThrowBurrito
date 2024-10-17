﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cliente.ServicioChat {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServicioChat.IServicioChat", CallbackContract=typeof(Cliente.ServicioChat.IServicioChatCallback))]
    public interface IServicioChat {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IServicioChat/Unirse")]
        void Unirse(string nombreUsuario);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IServicioChat/Unirse")]
        System.Threading.Tasks.Task UnirseAsync(string nombreUsuario);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IServicioChat/EnviarMensaje")]
        void EnviarMensaje(string nombreUsuario, string mensaje);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IServicioChat/EnviarMensaje")]
        System.Threading.Tasks.Task EnviarMensajeAsync(string nombreUsuario, string mensaje);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServicioChatCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IServicioChat/RecibirMensaje")]
        void RecibirMensaje(string mensajeCompleto);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServicioChatChannel : Cliente.ServicioChat.IServicioChat, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServicioChatClient : System.ServiceModel.DuplexClientBase<Cliente.ServicioChat.IServicioChat>, Cliente.ServicioChat.IServicioChat {
        
        public ServicioChatClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ServicioChatClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ServicioChatClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ServicioChatClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ServicioChatClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Unirse(string nombreUsuario) {
            base.Channel.Unirse(nombreUsuario);
        }
        
        public System.Threading.Tasks.Task UnirseAsync(string nombreUsuario) {
            return base.Channel.UnirseAsync(nombreUsuario);
        }
        
        public void EnviarMensaje(string nombreUsuario, string mensaje) {
            base.Channel.EnviarMensaje(nombreUsuario, mensaje);
        }
        
        public System.Threading.Tasks.Task EnviarMensajeAsync(string nombreUsuario, string mensaje) {
            return base.Channel.EnviarMensajeAsync(nombreUsuario, mensaje);
        }
    }
}
