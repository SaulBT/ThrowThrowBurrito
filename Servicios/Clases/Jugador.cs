using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.clases
{
    [DataContract]
    public class Jugador
    {
        private String descripcion;
        private byte[] fotoPerfil;
        private String correoElectronico;
        private String claveUsuario;
        private String contrasenia;
        private String estado;
        private String nombreUsuario;
        private Boolean esInvitado;

        public Jugador()
        {
        }

        public Jugador(string descripcion, byte[] fotoPerfil, string correoElectronico, string claveUsuario, string contrasenia, string estado, string nombreUsuario, bool esInvitado)
        {
            this.descripcion = descripcion;
            this.fotoPerfil = fotoPerfil;
            this.correoElectronico = correoElectronico;
            this.claveUsuario = claveUsuario;
            this.contrasenia = contrasenia;
            this.estado = estado;
            this.nombreUsuario = nombreUsuario;
            this.esInvitado = esInvitado;
        }

        [DataMember]
        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        [DataMember]
        public byte[] FotoPerfil
        {
            get { return fotoPerfil; }
            set { fotoPerfil = value; }
        }

        [DataMember]
        public String CorreoElectronico
        {
            get { return correoElectronico; }
            set { correoElectronico = value; }
        }

        [DataMember]
        public String ClaveUsuario
        {
            get { return claveUsuario; }
            set { claveUsuario = value; }
        }

        [DataMember]
        public String Contrasenia
        {
            get { return contrasenia; }
            set { contrasenia = value; }
        }

        [DataMember]
        public String Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        [DataMember]
        public String NombreUsuario
        {
            get { return nombreUsuario; }
            set { nombreUsuario = value; }
        }

        [DataMember]
        public Boolean EsInvitado
        {
            get { return esInvitado; }
            set { esInvitado = value; }
        }
    }
}
