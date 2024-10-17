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
        private Jugador jugador;
        private ServicioLogin.ServicioLoginClient servicio;

        public LogicaLogin()
        {
            servicio = new ServicioLoginClient();
        }

        public Jugador Jugador
        {
            get { return jugador; }
            set { jugador = value; }
        }


        public bool IniciarSesion(string nombreUsuario, string contrasenia)
        {
            jugador = servicio.Login(nombreUsuario, contrasenia);

            if (jugador != null)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
