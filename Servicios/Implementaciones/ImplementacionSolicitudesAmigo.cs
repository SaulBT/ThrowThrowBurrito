using AccesoDatos;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Implementaciones
{
    public class ImplementacionSolicitudesAmigo : IServicioSolicitudes
    {
        Dictionary<int, IServicioSolicitudesCallback> clientes = new Dictionary<int, IServicioSolicitudesCallback>();

        public bool EnviarSolicitud(string claveJugadorEmisor, string claveJugadorRemitente, int idJugador)
        {
            Jugador jugadorRemitente = buscarJugador(claveJugadorRemitente);
            if (jugadorRemitente != null)
            {
                if (!verificarBloqueo(claveJugadorEmisor, claveJugadorRemitente) && !verificarAmigo(idJugador, jugadorRemitente.idJugador)){
                    SolicitudAmigo solicitudAmigo = new SolicitudAmigo
                    {
                        idJugador = idJugador,
                        idAmigo = jugadorRemitente.idJugador,
                        estado = "Pendiente",
                        fecha = DateTime.Now,
                    };

                    using(var contexto = new ModeloDBContainer())
                    {
                        contexto.Database.Log = Console.WriteLine;
                        contexto.SolicitudAmigo.Add(solicitudAmigo);
                        contexto.SaveChanges();
                    }

                    actualizarSolicitudes(jugadorRemitente.idJugador, solicitudAmigo);

                    return true;
                }
            }
            
            return false;
        }

        private Jugador buscarJugador(string claveJugador)
        {
            using(var contexto = new ModeloDBContainer())
            {
                contexto.Database.Log = Console.WriteLine;
                var jugador = (from j in contexto.Jugador
                               where j.claveUsuario == claveJugador
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
                } else
                {
                    return null;
                }
            }
        }

        private bool verificarBloqueo(string claveJugadorEmisor, string claveJugadorRemitente)
        {
            using (var contexto = new ModeloDBContainer())
            {
                var yoBloqueado = (from b in contexto.SolicitudBloqueo
                               where b.claveJugadorEmisor == claveJugadorRemitente && b.claveJugadorBloqueado == claveJugadorEmisor
                               select b).FirstOrDefault();
                var yoBloquee = (from b in contexto.SolicitudBloqueo
                                 where b.claveJugadorEmisor == claveJugadorEmisor && b.claveJugadorBloqueado == claveJugadorRemitente
                                 select b).FirstOrDefault();

                if (yoBloqueado == null && yoBloquee == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private bool verificarAmigo(int idJugador, int idAmigo)
        {
            using(var contexto = new ModeloDBContainer())
            {
                var amigo = (from s in contexto.SolicitudAmigo
                             where s.idJugador == idJugador && s.idAmigo == idAmigo
                             select s).FirstOrDefault();

                if (amigo != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void actualizarSolicitudes(int idJuador, SolicitudAmigo solicitud)
        {
            if (clientes.ContainsKey(idJuador))
            {
                {
                    clientes[idJuador].ObtenerNuevaSolicitud(solicitud);
                }
            }
        }

        public SolicitudAmigo[] RecibirSolicitudes(int idJugador)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IServicioSolicitudesCallback>();
            clientes.Add(idJugador, callback);

            List<SolicitudAmigo> listaSolicitudes = new List<SolicitudAmigo>();
            using(var contexto = new ModeloDBContainer())
            {
                var solicitudes = (from s in contexto.SolicitudAmigo
                                   where s.idJugador == idJugador && s.estado == "Pendiente"
                                   select s);

                listaSolicitudes = solicitudes.ToList<SolicitudAmigo>();
            }

            return listaSolicitudes.ToArray<SolicitudAmigo>();
        }

        public void AceptarSolicitud(SolicitudAmigo solicitud)
        {
            solicitud.estado = "Aceptada";
            using(var contexto = new ModeloDBContainer())
            {
                contexto.Entry(solicitud).State = EntityState.Modified;
                contexto.SaveChanges();
            }
            actualizarAmigos(solicitud.idJugador);
            
        }

        private void crearAmistad(string claveJugadorAmigo)
        {
            using (var contexto = new ModeloDBContainer())
            {
                var amigo = (from a in contexto.Amigo
                             where a.claveUsuarioAmigo == claveJugadorAmigo
                             select a).FirstOrDefault();
            }

            Amigo amigo = new Amigo
            {
                claveUsuarioAmigo = claveJugadorAmigo
            };

            
        }

        private void crearSolicitudAmigo(int idJugadorEmisor, int idJugadorRemitente)

        private void actualizarAmigos(int idJuador)
        {
            if (clientes.ContainsKey(idJuador))
            {
                {
                    clientes[idJuador].ActualizarListaAmigos();
                }
            }
        }

        public void EnviarInvitacion(string codigoPartida, string codigoJugadorInvitado)
        {
            throw new NotImplementedException();
        }

        

        public void RechazarSolicitud(int idSolicitudAmigo)
        {
            throw new NotImplementedException();
        }

        
    }
}
