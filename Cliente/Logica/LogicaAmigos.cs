using Cliente.ServiciosGestionUsuarios;
using Cliente.Ventanas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Logica
{
    public class LogicaAmigos : IServicioSolicitudesCallback
    {
        private ServicioAmigosClient servicioAmigos = new ServicioAmigosClient();
        private ServicioSolicitudesClient servicioSolicitudes;
        public int idJugador { get; set; }
        private ObservableCollection<Jugador> amigos;
        private ObservableCollection<Jugador> solicitudes;
        private vntListaAmigos vntListaAmigos;

        public LogicaAmigos(int idJugador, vntListaAmigos vntListaAmigos)
        {
            this.idJugador = idJugador;
            this.vntListaAmigos = vntListaAmigos;
            InstanceContext contexto = new InstanceContext(this);
            servicioSolicitudes = new ServicioSolicitudesClient(contexto);
            amigos = new ObservableCollection<Jugador>(servicioAmigos.CargarAmigos(idJugador));
            
            cargarListaAmigos();
            cargarSolicitudes();
        }

        private void cargarListaAmigos()
        {
            vntListaAmigos.CargarListaAmigos(amigos);
        }

        private void cargarSolicitudes()
        {
            solicitudes = new ObservableCollection<Jugador>(servicioSolicitudes.RecibirSolicitudes(idJugador));
            bool notificaciones;
            if(solicitudes != null)
            {
                notificaciones = true;
                Console.WriteLine("Hay " + solicitudes.ToArray().Length + " solicitudes en el cliente");
            }
            else
            {
                notificaciones = false;
            }

            vntListaAmigos.configurarNotificaciones(notificaciones, solicitudes);
        }

        public bool EnviarSolicitudAmistad(string claveJugadorRemitente)
        {
            return servicioSolicitudes.EnviarSolicitud(claveJugadorRemitente, idJugador);
        }

        public void AceptarSolicitudAmistad(int idJugadorEmisor)
        {
            servicioSolicitudes.AceptarSolicitud(idJugadorEmisor, idJugador);
        }

        public void RechazarSolicitudAmistad(int idJugadorEmisor)
        {
            servicioSolicitudes.RechazarSolicitud(idJugadorEmisor, idJugador);
        }

        public void EliminarAmigo(int idJugadorReceptor)
        {
            servicioAmigos.EliminarAmigo(idJugador, idJugadorReceptor);
        }

        public void ObtenerNuevaSolicitud(Amigo solicitud)
        {
            throw new NotImplementedException();
        }

        public void ActualizarListaAmigos()
        {
            throw new NotImplementedException();
        }
    }
}
