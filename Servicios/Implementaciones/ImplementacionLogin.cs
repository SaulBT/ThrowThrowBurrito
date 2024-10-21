using AccesoDatos;
using Servicios.Interfaces;
using System;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Implementaciones
{
    public class ImplementacionLogin : IServicioLogin
    {
        public Jugador Login(string nombreUsuario, string contrasenia)
        {
            Jugador jugador = null;
            try
            {
                jugador = AccesoDatos.DAOJugador.buscarJugador(nombreUsuario, contrasenia);
            }
            catch (FaultException<ExcepcionServicioLogin>)
            {
                throw;
            }
            catch (Exception ex)
            {
                ExcepcionServicioLogin excepcionLogin = new ExcepcionServicioLogin
                {
                    Mensaje = "Ocurrió un error inesperado: " + ex.Message
                };
                throw new FaultException<ExcepcionServicioLogin>(excepcionLogin, new FaultReason("Error inesperado"));
            }

            return jugador;
        }
    }
}
