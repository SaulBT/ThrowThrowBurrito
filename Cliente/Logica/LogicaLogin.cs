using Cliente.ServiciosGestionUsuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Logica
{
    public class LogicaLogin
    {
        public ServicioLoginClient servicio;

        public LogicaLogin()
        {
            crearServicio();
        }

        private void crearServicio()
        {
            servicio = new ServicioLoginClient();
        }

        public Jugador IniciarSesion(string nombreUsuario, string contrasenia)
        {
            string contraseniaHash = encriptar(contrasenia);
            if (servicio.State == CommunicationState.Faulted)
            {
                crearServicio();
            }

            Jugador jugador = servicio.Login(nombreUsuario, contraseniaHash);
            return jugador;
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
