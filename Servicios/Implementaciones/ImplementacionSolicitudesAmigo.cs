using AccesoDatos;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Implementaciones
{
    public class ImplementacionSolicitudesAmigo : IServicioSolicitudes
    {
        public bool EnviarSolicitud(string claveJugadorEmisor, string claveJugadorRemitente, int idJugador)
        {
            Jugador jugadorRemitente = buscarJugador(claveJugadorRemitente);
            if (jugadorRemitente != null)
            {
                if (!verificarBloqueo(claveJugadorEmisor, claveJugadorRemitente) && !verificarAmigo(idJugador, jugadorRemitente.idJugador){
                    SolicitudAmigo solicitudAmigo = new SolicitudAmigo
                    {
                        idJugador = idJugador,
                        idAmigo = jugadorRemitente.idJugador,
                        estado = "Pendiente",
                        fecha = DateTime.Now,
                    };

                    //TO DO -> CARGAR LA SOLICITUD A BD.
                }
            }
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


        public void AceptarSolicitud(SolicitudAmigo solicitud)
        {
            throw new NotImplementedException();
        }

        public void EnviarInvitacion(string codigoPartida, string codigoJugadorInvitado)
        {
            throw new NotImplementedException();
        }

        

        public void RechazarSolicitud(int idSolicitudAmigo)
        {
            throw new NotImplementedException();
        }

        public SolicitudAmigo[] RecibirSolicitudes(string claveJugador)
        {
            throw new NotImplementedException();
        }
    }
}
