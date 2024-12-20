﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public static class DAOAmigo
    {
        public static Amigo ObtenerAmigo(int idJugadorEmisor, int idJugadorReceptor)
        {
            using(var contexto = new ModeloDBContainer())
            {
                var amigo = (from a in contexto.Amigo
                             where a.idJugadorEmisor == idJugadorEmisor && a.idJugadorReceptor == idJugadorReceptor
                             || a.idJugadorEmisor == idJugadorReceptor && a.idJugadorReceptor == idJugadorEmisor
                             select a).FirstOrDefault();

                if (amigo != null)
                {
                    Amigo respuesta = new Amigo
                    {
                        idAmigo = amigo.idAmigo,
                        idJugadorEmisor = amigo.idJugadorEmisor,
                        idJugadorReceptor = amigo.idJugadorReceptor,
                        estado = amigo.estado,
                    };

                    return respuesta;
                }
                else
                {
                    return null;
                }
            }
        }

        public static void CargarAmigo(Amigo solicitud)
        {
            using(var contexto = new ModeloDBContainer())
            {
                contexto.Amigo.Add(solicitud);
                contexto.SaveChanges();
            }
        }

        public static Jugador[] ObtenerSolicitudes(int idJugadorReceptor)
        {
            using (var contexto = new ModeloDBContainer())
            {
                List<Jugador> respuesta = new List<Jugador>();

                var amigos1 = (from a in contexto.Amigo
                               where a.Jugador1.idJugador == idJugadorReceptor && a.estado == "Pendiente"
                               select a.Jugador);

                if (amigos1 != null)
                {
                    foreach (var a in amigos1)
                    {
                        respuesta.Add(new Jugador
                        {
                            idJugador = a.idJugador,
                            fotoPerfil = a.fotoPerfil,
                            nombreUsuario = a.nombreUsuario,
                        });
                    }
                }

                return respuesta.ToArray();
            }
        }

        public static void AceptarSolicitud(Amigo solicitud)
        {
            using(var contexto = new ModeloDBContainer())
            {
                contexto.Entry(solicitud).State = System.Data.Entity.EntityState.Modified;
                contexto.SaveChanges();
            }
        }

        public static void RechazarSolicitud(int idAmigo)
        {
            using(var contexto = new ModeloDBContainer())
            {
                var solicitud = contexto.Amigo.Find(idAmigo);
                if (solicitud != null)
                {
                    contexto.Amigo.Remove(solicitud);
                    contexto.SaveChanges();
                }
            }
        }

        public static Jugador[] ObtenerAmigos(int idJugador)
        {
            using (var contexto = new ModeloDBContainer())
            {
                List<Jugador> respuesta = new List<Jugador>();

                var amigos1 = (from a in contexto.Amigo
                              where a.Jugador.idJugador == idJugador && a.estado == "Aceptada"
                              select a.Jugador1);

                var amigos2 = (from a in contexto.Amigo
                               where a.Jugador1.idJugador == idJugador && a.estado == "Aceptada"
                               select a.Jugador);

                if (amigos1 != null)
                {
                    foreach(var a in amigos1)
                    {
                        respuesta.Add(new Jugador
                        {
                            idJugador = a.idJugador,
                            fotoPerfil = a.fotoPerfil,
                            estado = a.estado,
                            correoElectronico = a.correoElectronico,
                            nombreUsuario = a.nombreUsuario,
                        });
                    }
                }
                if (amigos2 != null)
                {
                    foreach(var a in amigos2)
                    {
                        respuesta.Add(new Jugador
                        {
                            idJugador = a.idJugador,
                            fotoPerfil = a.fotoPerfil,
                            estado = a.estado,
                            correoElectronico = a.correoElectronico,
                            nombreUsuario = a.nombreUsuario,
                        });
                    }
                }

                return respuesta.ToArray();
            }
        }

        public static void EliminarAmigo(int idAmigo)
        {
            using (var contexto = new ModeloDBContainer())
            {
                var amigo = contexto.Amigo.Find(idAmigo);
                if (amigo != null)
                {
                    contexto.Amigo.Remove(amigo);
                    contexto.SaveChanges();
                }
            }
        }
    }
}
