﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cliente.ServicioLogin {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Jugador", Namespace="http://schemas.datacontract.org/2004/07/AccesoDatos")]
    [System.SerializableAttribute()]
    public partial class Jugador : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Cliente.ServicioLogin.DatosJugadorPartida[] DatosJugadorPartidaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Cliente.ServicioLogin.SolicitudAmigo[] SolicitudAmigoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string claveUsuarioField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string contraseniaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string correoElectronicoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string descripcionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<bool> esInvitadoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string estadoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] fotoPerfilField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int idJugadorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string nombreUsuarioField;
        
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
        public Cliente.ServicioLogin.DatosJugadorPartida[] DatosJugadorPartida {
            get {
                return this.DatosJugadorPartidaField;
            }
            set {
                if ((object.ReferenceEquals(this.DatosJugadorPartidaField, value) != true)) {
                    this.DatosJugadorPartidaField = value;
                    this.RaisePropertyChanged("DatosJugadorPartida");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Cliente.ServicioLogin.SolicitudAmigo[] SolicitudAmigo {
            get {
                return this.SolicitudAmigoField;
            }
            set {
                if ((object.ReferenceEquals(this.SolicitudAmigoField, value) != true)) {
                    this.SolicitudAmigoField = value;
                    this.RaisePropertyChanged("SolicitudAmigo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string claveUsuario {
            get {
                return this.claveUsuarioField;
            }
            set {
                if ((object.ReferenceEquals(this.claveUsuarioField, value) != true)) {
                    this.claveUsuarioField = value;
                    this.RaisePropertyChanged("claveUsuario");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string contrasenia {
            get {
                return this.contraseniaField;
            }
            set {
                if ((object.ReferenceEquals(this.contraseniaField, value) != true)) {
                    this.contraseniaField = value;
                    this.RaisePropertyChanged("contrasenia");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string correoElectronico {
            get {
                return this.correoElectronicoField;
            }
            set {
                if ((object.ReferenceEquals(this.correoElectronicoField, value) != true)) {
                    this.correoElectronicoField = value;
                    this.RaisePropertyChanged("correoElectronico");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string descripcion {
            get {
                return this.descripcionField;
            }
            set {
                if ((object.ReferenceEquals(this.descripcionField, value) != true)) {
                    this.descripcionField = value;
                    this.RaisePropertyChanged("descripcion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> esInvitado {
            get {
                return this.esInvitadoField;
            }
            set {
                if ((this.esInvitadoField.Equals(value) != true)) {
                    this.esInvitadoField = value;
                    this.RaisePropertyChanged("esInvitado");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string estado {
            get {
                return this.estadoField;
            }
            set {
                if ((object.ReferenceEquals(this.estadoField, value) != true)) {
                    this.estadoField = value;
                    this.RaisePropertyChanged("estado");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] fotoPerfil {
            get {
                return this.fotoPerfilField;
            }
            set {
                if ((object.ReferenceEquals(this.fotoPerfilField, value) != true)) {
                    this.fotoPerfilField = value;
                    this.RaisePropertyChanged("fotoPerfil");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int idJugador {
            get {
                return this.idJugadorField;
            }
            set {
                if ((this.idJugadorField.Equals(value) != true)) {
                    this.idJugadorField = value;
                    this.RaisePropertyChanged("idJugador");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string nombreUsuario {
            get {
                return this.nombreUsuarioField;
            }
            set {
                if ((object.ReferenceEquals(this.nombreUsuarioField, value) != true)) {
                    this.nombreUsuarioField = value;
                    this.RaisePropertyChanged("nombreUsuario");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DatosJugadorPartida", Namespace="http://schemas.datacontract.org/2004/07/AccesoDatos")]
    [System.SerializableAttribute()]
    public partial class DatosJugadorPartida : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Cliente.ServicioLogin.Jugador JugadorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Cliente.ServicioLogin.Partida PartidaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<bool> expulsadoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> idAspectoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int idDatosJugadorPartidaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int idJugadorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int idPartidaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> puntajeField;
        
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
        public Cliente.ServicioLogin.Jugador Jugador {
            get {
                return this.JugadorField;
            }
            set {
                if ((object.ReferenceEquals(this.JugadorField, value) != true)) {
                    this.JugadorField = value;
                    this.RaisePropertyChanged("Jugador");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Cliente.ServicioLogin.Partida Partida {
            get {
                return this.PartidaField;
            }
            set {
                if ((object.ReferenceEquals(this.PartidaField, value) != true)) {
                    this.PartidaField = value;
                    this.RaisePropertyChanged("Partida");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> expulsado {
            get {
                return this.expulsadoField;
            }
            set {
                if ((this.expulsadoField.Equals(value) != true)) {
                    this.expulsadoField = value;
                    this.RaisePropertyChanged("expulsado");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> idAspecto {
            get {
                return this.idAspectoField;
            }
            set {
                if ((this.idAspectoField.Equals(value) != true)) {
                    this.idAspectoField = value;
                    this.RaisePropertyChanged("idAspecto");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int idDatosJugadorPartida {
            get {
                return this.idDatosJugadorPartidaField;
            }
            set {
                if ((this.idDatosJugadorPartidaField.Equals(value) != true)) {
                    this.idDatosJugadorPartidaField = value;
                    this.RaisePropertyChanged("idDatosJugadorPartida");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int idJugador {
            get {
                return this.idJugadorField;
            }
            set {
                if ((this.idJugadorField.Equals(value) != true)) {
                    this.idJugadorField = value;
                    this.RaisePropertyChanged("idJugador");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int idPartida {
            get {
                return this.idPartidaField;
            }
            set {
                if ((this.idPartidaField.Equals(value) != true)) {
                    this.idPartidaField = value;
                    this.RaisePropertyChanged("idPartida");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> puntaje {
            get {
                return this.puntajeField;
            }
            set {
                if ((this.puntajeField.Equals(value) != true)) {
                    this.puntajeField = value;
                    this.RaisePropertyChanged("puntaje");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SolicitudAmigo", Namespace="http://schemas.datacontract.org/2004/07/AccesoDatos")]
    [System.SerializableAttribute()]
    public partial class SolicitudAmigo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Cliente.ServicioLogin.Amigo AmigoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Cliente.ServicioLogin.Jugador JugadorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string estadoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> fechaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int idAmigoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int idJugadorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int idSolicitudAmigoField;
        
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
        public Cliente.ServicioLogin.Amigo Amigo {
            get {
                return this.AmigoField;
            }
            set {
                if ((object.ReferenceEquals(this.AmigoField, value) != true)) {
                    this.AmigoField = value;
                    this.RaisePropertyChanged("Amigo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Cliente.ServicioLogin.Jugador Jugador {
            get {
                return this.JugadorField;
            }
            set {
                if ((object.ReferenceEquals(this.JugadorField, value) != true)) {
                    this.JugadorField = value;
                    this.RaisePropertyChanged("Jugador");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string estado {
            get {
                return this.estadoField;
            }
            set {
                if ((object.ReferenceEquals(this.estadoField, value) != true)) {
                    this.estadoField = value;
                    this.RaisePropertyChanged("estado");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> fecha {
            get {
                return this.fechaField;
            }
            set {
                if ((this.fechaField.Equals(value) != true)) {
                    this.fechaField = value;
                    this.RaisePropertyChanged("fecha");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int idAmigo {
            get {
                return this.idAmigoField;
            }
            set {
                if ((this.idAmigoField.Equals(value) != true)) {
                    this.idAmigoField = value;
                    this.RaisePropertyChanged("idAmigo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int idJugador {
            get {
                return this.idJugadorField;
            }
            set {
                if ((this.idJugadorField.Equals(value) != true)) {
                    this.idJugadorField = value;
                    this.RaisePropertyChanged("idJugador");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int idSolicitudAmigo {
            get {
                return this.idSolicitudAmigoField;
            }
            set {
                if ((this.idSolicitudAmigoField.Equals(value) != true)) {
                    this.idSolicitudAmigoField = value;
                    this.RaisePropertyChanged("idSolicitudAmigo");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Partida", Namespace="http://schemas.datacontract.org/2004/07/AccesoDatos")]
    [System.SerializableAttribute()]
    public partial class Partida : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Cliente.ServicioLogin.DatosJugadorPartida[] DatosJugadorPartidaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string codigoPartidaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> duracionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string estadoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> fechaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int idPartidaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string nombreGanadorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> puntajeVictoriaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> tiempoGuerraField;
        
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
        public Cliente.ServicioLogin.DatosJugadorPartida[] DatosJugadorPartida {
            get {
                return this.DatosJugadorPartidaField;
            }
            set {
                if ((object.ReferenceEquals(this.DatosJugadorPartidaField, value) != true)) {
                    this.DatosJugadorPartidaField = value;
                    this.RaisePropertyChanged("DatosJugadorPartida");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string codigoPartida {
            get {
                return this.codigoPartidaField;
            }
            set {
                if ((object.ReferenceEquals(this.codigoPartidaField, value) != true)) {
                    this.codigoPartidaField = value;
                    this.RaisePropertyChanged("codigoPartida");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> duracion {
            get {
                return this.duracionField;
            }
            set {
                if ((this.duracionField.Equals(value) != true)) {
                    this.duracionField = value;
                    this.RaisePropertyChanged("duracion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string estado {
            get {
                return this.estadoField;
            }
            set {
                if ((object.ReferenceEquals(this.estadoField, value) != true)) {
                    this.estadoField = value;
                    this.RaisePropertyChanged("estado");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> fecha {
            get {
                return this.fechaField;
            }
            set {
                if ((this.fechaField.Equals(value) != true)) {
                    this.fechaField = value;
                    this.RaisePropertyChanged("fecha");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int idPartida {
            get {
                return this.idPartidaField;
            }
            set {
                if ((this.idPartidaField.Equals(value) != true)) {
                    this.idPartidaField = value;
                    this.RaisePropertyChanged("idPartida");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string nombreGanador {
            get {
                return this.nombreGanadorField;
            }
            set {
                if ((object.ReferenceEquals(this.nombreGanadorField, value) != true)) {
                    this.nombreGanadorField = value;
                    this.RaisePropertyChanged("nombreGanador");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> puntajeVictoria {
            get {
                return this.puntajeVictoriaField;
            }
            set {
                if ((this.puntajeVictoriaField.Equals(value) != true)) {
                    this.puntajeVictoriaField = value;
                    this.RaisePropertyChanged("puntajeVictoria");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> tiempoGuerra {
            get {
                return this.tiempoGuerraField;
            }
            set {
                if ((this.tiempoGuerraField.Equals(value) != true)) {
                    this.tiempoGuerraField = value;
                    this.RaisePropertyChanged("tiempoGuerra");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Amigo", Namespace="http://schemas.datacontract.org/2004/07/AccesoDatos")]
    [System.SerializableAttribute()]
    public partial class Amigo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Cliente.ServicioLogin.SolicitudAmigo[] SolicitudAmigoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string claveUsuarioAmigoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int idAmigoField;
        
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
        public Cliente.ServicioLogin.SolicitudAmigo[] SolicitudAmigo {
            get {
                return this.SolicitudAmigoField;
            }
            set {
                if ((object.ReferenceEquals(this.SolicitudAmigoField, value) != true)) {
                    this.SolicitudAmigoField = value;
                    this.RaisePropertyChanged("SolicitudAmigo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string claveUsuarioAmigo {
            get {
                return this.claveUsuarioAmigoField;
            }
            set {
                if ((object.ReferenceEquals(this.claveUsuarioAmigoField, value) != true)) {
                    this.claveUsuarioAmigoField = value;
                    this.RaisePropertyChanged("claveUsuarioAmigo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int idAmigo {
            get {
                return this.idAmigoField;
            }
            set {
                if ((this.idAmigoField.Equals(value) != true)) {
                    this.idAmigoField = value;
                    this.RaisePropertyChanged("idAmigo");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServicioLogin.IServicioLogin")]
    public interface IServicioLogin {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioLogin/Login", ReplyAction="http://tempuri.org/IServicioLogin/LoginResponse")]
        Cliente.ServicioLogin.Jugador Login(string nombreUsuario, string contrasenia);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServicioLogin/Login", ReplyAction="http://tempuri.org/IServicioLogin/LoginResponse")]
        System.Threading.Tasks.Task<Cliente.ServicioLogin.Jugador> LoginAsync(string nombreUsuario, string contrasenia);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServicioLoginChannel : Cliente.ServicioLogin.IServicioLogin, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServicioLoginClient : System.ServiceModel.ClientBase<Cliente.ServicioLogin.IServicioLogin>, Cliente.ServicioLogin.IServicioLogin {
        
        public ServicioLoginClient() {
        }
        
        public ServicioLoginClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServicioLoginClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServicioLoginClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServicioLoginClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Cliente.ServicioLogin.Jugador Login(string nombreUsuario, string contrasenia) {
            return base.Channel.Login(nombreUsuario, contrasenia);
        }
        
        public System.Threading.Tasks.Task<Cliente.ServicioLogin.Jugador> LoginAsync(string nombreUsuario, string contrasenia) {
            return base.Channel.LoginAsync(nombreUsuario, contrasenia);
        }
    }
}
