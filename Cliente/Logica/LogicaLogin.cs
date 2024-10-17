using Cliente.ServicioLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Logica
{
    public class LogicaLogin
    {
        private ServicioLogin.ServicioLoginClient servicio;

        public LogicaLogin()
        {
            servicio = new ServicioLoginClient();
        }

        public Jugador IniciarSesion(string nombreUsuario, string contrasenia)
        {
            Jugador jugador = servicio.Login(nombreUsuario, contrasenia);

            return jugador;
        }
    }
}
