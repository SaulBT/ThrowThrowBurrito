﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cliente.Servidor {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Usuario", Namespace="http://schemas.datacontract.org/2004/07/Servicios")]
    [System.SerializableAttribute()]
    public partial class Usuario : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContraseniaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CorreoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NombreUsuarioField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Contrasenia {
            get {
                return this.ContraseniaField;
            }
            set {
                if ((object.ReferenceEquals(this.ContraseniaField, value) != true)) {
                    this.ContraseniaField = value;
                    this.RaisePropertyChanged("Contrasenia");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Correo {
            get {
                return this.CorreoField;
            }
            set {
                if ((object.ReferenceEquals(this.CorreoField, value) != true)) {
                    this.CorreoField = value;
                    this.RaisePropertyChanged("Correo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NombreUsuario {
            get {
                return this.NombreUsuarioField;
            }
            set {
                if ((object.ReferenceEquals(this.NombreUsuarioField, value) != true)) {
                    this.NombreUsuarioField = value;
                    this.RaisePropertyChanged("NombreUsuario");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Servidor.IServicioRegistrarUsuario")]
    public interface IServicioRegistrarUsuario {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioRegistrarUsuario/ValidarDatos", ReplyAction="http://tempuri.org/IServicioRegistrarUsuario/ValidarDatosResponse")]
        bool ValidarDatos(Cliente.Servidor.Usuario usuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioRegistrarUsuario/ValidarDatos", ReplyAction="http://tempuri.org/IServicioRegistrarUsuario/ValidarDatosResponse")]
        System.Threading.Tasks.Task<bool> ValidarDatosAsync(Cliente.Servidor.Usuario usuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioRegistrarUsuario/ValidarCorreo", ReplyAction="http://tempuri.org/IServicioRegistrarUsuario/ValidarCorreoResponse")]
        bool ValidarCorreo(string codigoCorreo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioRegistrarUsuario/ValidarCorreo", ReplyAction="http://tempuri.org/IServicioRegistrarUsuario/ValidarCorreoResponse")]
        System.Threading.Tasks.Task<bool> ValidarCorreoAsync(string codigoCorreo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioRegistrarUsuario/EnviarCodigoCorreo", ReplyAction="http://tempuri.org/IServicioRegistrarUsuario/EnviarCodigoCorreoResponse")]
        string EnviarCodigoCorreo(string correo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioRegistrarUsuario/EnviarCodigoCorreo", ReplyAction="http://tempuri.org/IServicioRegistrarUsuario/EnviarCodigoCorreoResponse")]
        System.Threading.Tasks.Task<string> EnviarCodigoCorreoAsync(string correo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioRegistrarUsuario/RegistrarUsuario", ReplyAction="http://tempuri.org/IServicioRegistrarUsuario/RegistrarUsuarioResponse")]
        bool RegistrarUsuario(Cliente.Servidor.Usuario usuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioRegistrarUsuario/RegistrarUsuario", ReplyAction="http://tempuri.org/IServicioRegistrarUsuario/RegistrarUsuarioResponse")]
        System.Threading.Tasks.Task<bool> RegistrarUsuarioAsync(Cliente.Servidor.Usuario usuario);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServicioRegistrarUsuarioChannel : Cliente.Servidor.IServicioRegistrarUsuario, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServicioRegistrarUsuarioClient : System.ServiceModel.ClientBase<Cliente.Servidor.IServicioRegistrarUsuario>, Cliente.Servidor.IServicioRegistrarUsuario {
        
        public ServicioRegistrarUsuarioClient() {
        }
        
        public ServicioRegistrarUsuarioClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServicioRegistrarUsuarioClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServicioRegistrarUsuarioClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServicioRegistrarUsuarioClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool ValidarDatos(Cliente.Servidor.Usuario usuario) {
            return base.Channel.ValidarDatos(usuario);
        }
        
        public System.Threading.Tasks.Task<bool> ValidarDatosAsync(Cliente.Servidor.Usuario usuario) {
            return base.Channel.ValidarDatosAsync(usuario);
        }
        
        public bool ValidarCorreo(string codigoCorreo) {
            return base.Channel.ValidarCorreo(codigoCorreo);
        }
        
        public System.Threading.Tasks.Task<bool> ValidarCorreoAsync(string codigoCorreo) {
            return base.Channel.ValidarCorreoAsync(codigoCorreo);
        }
        
        public string EnviarCodigoCorreo(string correo) {
            return base.Channel.EnviarCodigoCorreo(correo);
        }
        
        public System.Threading.Tasks.Task<string> EnviarCodigoCorreoAsync(string correo) {
            return base.Channel.EnviarCodigoCorreoAsync(correo);
        }
        
        public bool RegistrarUsuario(Cliente.Servidor.Usuario usuario) {
            return base.Channel.RegistrarUsuario(usuario);
        }
        
        public System.Threading.Tasks.Task<bool> RegistrarUsuarioAsync(Cliente.Servidor.Usuario usuario) {
            return base.Channel.RegistrarUsuarioAsync(usuario);
        }
    }
}
