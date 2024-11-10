using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Cliente.ServiciosGestionUsuarios;

namespace Cliente.Logica
{
    public class LogicaCrearJugador
    {
        private ServicioRegistrarUsuarioClient servicio;

        public LogicaCrearJugador()
        {
            servicio = new ServicioRegistrarUsuarioClient();
        }

        public void crearUsuario(Usuario usuario)
        {
            string contrasenia = Utilidades.encriptar(usuario.Contrasenia);
            usuario.Contrasenia = contrasenia;
            servicio.RegistrarUsuario(usuario);
        }

    }
}
