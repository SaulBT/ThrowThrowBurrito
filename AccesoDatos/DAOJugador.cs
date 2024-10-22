using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public static class DAOJugador
    {
        public static Jugador buscarJugador(String nombreUsuario, String contrasenia)
        {
            using (var contexto = new ModeloDBContainer())
            {
                contexto.Database.Log = Console.WriteLine;
                Jugador jugador = (from j in contexto.Jugador
                                   where j.nombreUsuario == nombreUsuario && j.contrasenia == contrasenia
                                   select j).FirstOrDefault();
                return jugador;
            }
            

            
        }
    }
}
