using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AccesoDatos;

namespace Servicios
{
    [ServiceContract]
    public interface IServicioRegistrarUsuario
    {
        [OperationContract]
        bool ValidarDatos(Usuario usuario);
        [OperationContract]
        string EnviarCodigoCorreo(string correo);
        [OperationContract]
        bool RegistrarUsuario(Usuario usuario);
    }

    [DataContract]
    public class Usuario
    {
        private string nombreUsuario;
        private string contrasenia;
        private string correo;

       

        [DataMember]
        public string NombreUsuario { get { return nombreUsuario; } set { nombreUsuario = value; } }

        [DataMember]
        public string Contrasenia { get { return contrasenia; } set { contrasenia = value; } }

        [DataMember]
        public string Correo { get { return correo; } set { correo = value; } }
    }
}
