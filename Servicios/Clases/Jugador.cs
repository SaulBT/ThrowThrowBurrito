using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.clases
{
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

        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public byte[] FotoPerfil
        {
            get { return fotoPerfil; }
            set { fotoPerfil = value; }
        }

    }
}
