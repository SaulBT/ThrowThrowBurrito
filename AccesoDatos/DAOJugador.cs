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
                var jugador = (from j in contexto.Jugador
                                             where j.nombreUsuario == nombreUsuario && j.contrasenia == contrasenia
                                             && j.nombreUsuario.Equals(nombreUsuario, StringComparison.Ordinal)
                                             && j.contrasenia.Equals(contrasenia, StringComparison.Ordinal)
                                             select j).FirstOrDefault();

                Jugador respuesta = new Jugador
                {
                    claveUsuario = jugador.claveUsuario,
                    descripcion = jugador.descripcion,
                    fotoPerfil = jugador.fotoPerfil,
                    correoElectronico = jugador.correoElectronico,
                    contrasenia = jugador.contrasenia,
                    estado = jugador.estado,
                    nombreUsuario = jugador.nombreUsuario,
                    esInvitado = jugador.esInvitado
                };

                return respuesta;
            }
            

            
        }
    }
}
