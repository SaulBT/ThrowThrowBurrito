using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Servicios
{
    [ServiceContract]
    public interface IServicioRegistrarUsuario
    {
        bool ValidarDatos(Usuario usuario);
        bool ValidarCorreo(string codigoCorreo);
        bool EnviarCodigoCorreo(string correo);
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
