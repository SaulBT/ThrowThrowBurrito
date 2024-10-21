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
            crearServicio();
        }

        private void crearServicio()
        {
            servicio = new ServicioLoginClient();
        }

        public Jugador IniciarSesion(string nombreUsuario, string contrasenia)
        {
            if (servicio.State == CommunicationState.Faulted)
            {
                crearServicio();
            }

            try
            {
                Jugador jugador = servicio.Login(nombreUsuario, contrasenia);
                return jugador;
            }
            catch (FaultException<ExcepcionServicioLogin>)
            {
                throw;
            }
           
        }
    }
}
