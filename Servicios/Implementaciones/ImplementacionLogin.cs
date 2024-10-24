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
            try
            {
                Jugador jugador = null;
                jugador = AccesoDatos.DAOJugador.buscarJugador(nombreUsuario, contrasenia);

                return jugador;
            }
            
            catch (EntityException ex)
            {
                throw new FaultException("Error de la base de datos");
            }
        }
    }
}
