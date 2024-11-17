using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public static class DAOJugador
    {
        public static void CrearJugador(Jugador jugador)
        {
            using (var contexto = new ModeloDBContainer())
            {
                contexto.Database.Log = Console.WriteLine;
                contexto.Jugador.Add(jugador);
                contexto.SaveChanges();
            }
        }

        public static Jugador ObtenerJugadorPorNombre(string nombreUsuario)
        {
            using (var contexto = new ModeloDBContainer())
            {
                contexto.Database.Log = Console.WriteLine;
                var jugador = (from j in contexto.Jugador
                               where j.nombreUsuario == nombreUsuario
                               select j).FirstOrDefault();

                if (jugador != null)
                {
                    Jugador respuesta = new Jugador
                    {
                        nombreUsuario = jugador.nombreUsuario,
                        claveUsuario = jugador.claveUsuario,
                        fotoPerfil = jugador.fotoPerfil,
                        contrasenia = jugador.contrasenia,
                        descripcion = jugador.descripcion,
                        esInvitado = jugador.esInvitado,
                        estado = jugador.estado,
                        correoElectronico = jugador.correoElectronico,
                        idJugador = jugador.idJugador,
                    };

                    return respuesta;
                }
                else
                {
                    return null;
                }
            }
        }

        public static Jugador ObtenerJugadorPorClave(string claveJugador)
        {
            using (var contexto = new ModeloDBContainer())
            {
                contexto.Database.Log = Console.WriteLine;
                var jugador = (from j in contexto.Jugador
                               where j.claveUsuario == claveJugador
                               select j).FirstOrDefault();

                if (jugador != null)
                {
                    Jugador respuesta = new Jugador
                    {
                        nombreUsuario = jugador.nombreUsuario,
                        claveUsuario = jugador.claveUsuario,
                        fotoPerfil = jugador.fotoPerfil,
                        contrasenia = jugador.contrasenia,
                        descripcion = jugador.descripcion,
                        esInvitado = jugador.esInvitado,
                        estado = jugador.estado,
                        correoElectronico = jugador.correoElectronico,
                        idJugador = jugador.idJugador,
                    };

                    return respuesta;
                }
                else
                {
                    return null;
                }
            }
        }

        public static Jugador ObtenerJugadorPorCorreo(string correo)
        {
            using (var contexto = new ModeloDBContainer())
            {
                contexto.Database.Log = Console.WriteLine;
                var jugador = (from j in contexto.Jugador
                               where j.correoElectronico == correo
                               select j).FirstOrDefault();

                if(jugador != null)
                {
                    Jugador respuesta = new Jugador
                    {
                        nombreUsuario = jugador.nombreUsuario,
                        claveUsuario = jugador.claveUsuario,
                        fotoPerfil = jugador.fotoPerfil,
                        contrasenia = jugador.contrasenia,
                        descripcion = jugador.descripcion,
                        esInvitado = jugador.esInvitado,
                        estado = jugador.estado,
                        correoElectronico = jugador.correoElectronico,
                        idJugador = jugador.idJugador,
                    };

                    return respuesta;
                } else
                {
                    return null;
                }
            }
        }

        public static void ActualizarJugador(Jugador jugador)
        {
            using (var contexto = new ModeloDBContainer())
            {
                contexto.Database.Log = Console.WriteLine;
                contexto.Entry(jugador).State = EntityState.Modified;
                contexto.SaveChanges();
            }
        }

        public static Jugador ObtenerJugadorPorNombreContrasenia(String nombreUsuario, String contrasenia)
        {
            using (var contexto = new ModeloDBContainer())
            {
                contexto.Database.Log = Console.WriteLine;
                var jugador = (from j in contexto.Jugador
                                             where j.nombreUsuario == nombreUsuario && j.contrasenia == contrasenia
                                             && j.nombreUsuario.Equals(nombreUsuario, StringComparison.Ordinal)
                                             && j.contrasenia.Equals(contrasenia, StringComparison.Ordinal)
                                             select j).FirstOrDefault();
                if (jugador != null)
                {
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
                else
                {
                    return null;
                }
            }
            

            
        }
    }
}
