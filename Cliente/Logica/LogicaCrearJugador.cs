using Cliente.ServicioRegistrarUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
            string contrasenia = encriptar(usuario.Contrasenia);
            usuario.Contrasenia = contrasenia;
            servicio.RegistrarUsuario(usuario);
        }

        private string encriptar(string contrasenia)
        {
            SHA256Managed sHA256Managed = new SHA256Managed();
            string contraHash = String.Empty;
            byte[] contraByte = sHA256Managed.ComputeHash(Encoding.UTF8.GetBytes(contrasenia));

            foreach (byte b in contraByte)
            {
                contraHash += b.ToString("x2");
            }

            return contraHash;
        }
    }
}
