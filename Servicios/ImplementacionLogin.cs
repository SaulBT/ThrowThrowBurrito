using AccesoDatos;
using System;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ImplementacionLogin : IServicioLogin
    {
        public Jugador Login(string nombreUsuario, string contrasenia)
        {
            Jugador jugador = null;
            try
            {
                using (var contexto = new ModeloDBContainer())
                {
                    contexto.Database.Log = Console.WriteLine;

                    jugador = ((Jugador)(from j in contexto.Jugador
                               where j.nombreUsuario == nombreUsuario && j.contrasenia == contrasenia
                               select j).FirstOrDefault());
                }
            }
            catch (EntityException ex)
            {
                ExcepcionServicioLogin excepcionLogin = new ExcepcionServicioLogin();
                excepcionLogin.Mensaje = "Error: \n" + ex.Message;
                throw new FaultException<ExcepcionServicioLogin>(excepcionLogin);
            }

            return jugador;
        }
    }
}
