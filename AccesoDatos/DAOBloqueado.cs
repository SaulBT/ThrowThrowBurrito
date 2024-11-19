using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public static class DAOBloqueado
    {
        public static Bloqueado ObtenerBloqueo(int idJugadorEmisor, int idJugadorReceptor)
        {
            using (var contexto = new ModeloDBContainer())
            {
                var bloqueado = (from b in contexto.Bloqueado
                                 where b.idJugadorEmisor == idJugadorEmisor && b.idJugadorReceptor == idJugadorReceptor
                                 select b).FirstOrDefault();
                if (bloqueado != null)
                {
                    Bloqueado respuesta = new Bloqueado
                    {
                        idBloqueado = bloqueado.idBloqueado,
                        idJugadorEmisor = bloqueado.idJugadorEmisor,
                        idJugadorReceptor = bloqueado.idJugadorReceptor,
                    };

                    return respuesta;
                }

                bloqueado = (from b in contexto.Bloqueado
                             where b.idJugadorReceptor == idJugadorEmisor && b.idJugadorEmisor == idJugadorReceptor
                             select b).FirstOrDefault();

                if (bloqueado != null)
                {
                    Bloqueado respuesta = new Bloqueado
                    {
                        idBloqueado = bloqueado.idBloqueado,
                        idJugadorEmisor = bloqueado.idJugadorEmisor,
                        idJugadorReceptor = bloqueado.idJugadorReceptor,
                    };

                    return respuesta;
                }

                return null;
            }
        }

        public static void Bloquear(Bloqueado bloqueado)
        {
            using (var contexto = new ModeloDBContainer())
            {
                contexto.Bloqueado.Add(bloqueado);
                contexto.SaveChanges();
            }
        }

        public static Bloqueado ObtenerBloqueado(int idJugadorEmisor, int idJugadorReceptor)
        {
            using (var contexto = new ModeloDBContainer())
            {
                var bloqueado = (from b in contexto.Bloqueado
                                 where b.idJugadorEmisor == idJugadorEmisor && b.idJugadorReceptor == idJugadorReceptor
                                 select b).FirstOrDefault();

                if (bloqueado != null)
                {
                    Bloqueado respuesta = new Bloqueado
                    {
                        idBloqueado = bloqueado.idBloqueado,
                        idJugadorEmisor = bloqueado.idJugadorEmisor,
                        idJugadorReceptor = bloqueado.idJugadorReceptor,
                    };

                    return respuesta;
                }
                else
                {
                    return null;
                }
            }
        }

        public static void Desbloquear(Bloqueado bloqueado)
        {
            using (var contexto = new ModeloDBContainer())
            {
                contexto.Bloqueado.Remove(bloqueado);
                contexto.SaveChanges();
            }
        }

        public static Jugador[] ObtenerJugadoresBloqueados(int idJugadorEmisor)
        {
            using (var contexto = new ModeloDBContainer())
            {
                var bloqueados = (from b in contexto.Bloqueado
                                  where b.idJugadorEmisor == idJugadorEmisor
                                  select b.Jugador);

                if (bloqueados != null)
                {
                    List<Jugador> respuesta = new List<Jugador>();
                    foreach (var b in bloqueados)
                    {
                        respuesta.Add(new Jugador
                        {
                            idJugador = b.idJugador,
                            fotoPerfil = b.fotoPerfil,
                            nombreUsuario = b.nombreUsuario,
                        });
                    }
                    return respuesta.ToArray();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
