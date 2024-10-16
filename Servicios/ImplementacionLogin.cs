using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    internal class ImplementacionLogin : IServicioLogin
    {
        public Dictionary<bool,object> Login(string nombreUsuario, string contrasenia)
        {
            Dictionary<bool, object> resultado = new Dictionary<bool, object>();
            try
            {
                using (var contexto = new ModeloDBContainer())
                {
                    contexto.Database.Log = Console.WriteLine;

                    var jugador = (from j in contexto.Jugador
                                   where j.nombreUsuario == nombreUsuario && j.contrasenia == contrasenia
                                   select j);
                    
                    resultado.Add(true, jugador);
                }
            }
            catch (EntityException ex)
            { 
                Console.WriteLine(ex.Message);
                resultado.Add(false, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                resultado.Add(false, ex.Message);
            }

            return resultado;
        }
    }
}
