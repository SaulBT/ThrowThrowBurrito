﻿using Cliente.ServiciosGestionUsuarios;
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
        private ObservableCollection<Jugador> bloqueados;
        private vntListaAmigos vntListaAmigos;

        public LogicaAmigos(int idJugador)
        {
            this.idJugador = idJugador;
            
            InstanceContext contexto = new InstanceContext(this);
            servicioSolicitudes = new ServicioSolicitudesClient(contexto);
            servicioSolicitudes.EnviarCliente(idJugador);

            amigos = new ObservableCollection<Jugador>(servicioAmigos.CargarAmigos(idJugador));
            solicitudes = new ObservableCollection<Jugador>(servicioSolicitudes.RecibirSolicitudes(idJugador));
            bloqueados = new ObservableCollection<Jugador>(servicioAmigos.CargarBloqueados(idJugador));

            
        }

        public void CargarVentanaAmigos(vntListaAmigos vntListaAmigos)
        {
            this.vntListaAmigos = vntListaAmigos;
            CargarListaAmigos();
            CargarSolicitudes();
            CargarBloqueados();
        }

        public void CargarListaAmigos()
        {
            
            vntListaAmigos.CargarListaAmigos(amigos);
        }

        public void CargarSolicitudes()
        {
            
            bool notificaciones;
            if(solicitudes.Count > 0)
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

        public void CargarBloqueados()
        {
            
            vntListaAmigos.uscBloqueados.AgregarBloqueados(bloqueados);
        }

        public int EnviarSolicitudAmistad(string claveJugadorRemitente)
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

        public void EnviarInvitacion(string codigoPartida, string correo, string nombreUsuario)
        {
            servicioSolicitudes.EnviarInvitacion(codigoPartida, correo, nombreUsuario);
        }

        public void EliminarAmigo(int idJugadorReceptor)
        {
            servicioAmigos.EliminarAmigo(idJugador, idJugadorReceptor);
        }

        public int BloquearJugador(string claveJugadorReceptor)
        {
            return servicioAmigos.BloquearJugador(idJugador, claveJugadorReceptor);
        }

        public void DesbloquearJugador(int idJugadorReceptor)
        {
            servicioAmigos.DesbloquearJugador(idJugador, idJugadorReceptor);
        }

        public void ObtenerNuevaSolicitud(Jugador solicitud)
        {
            solicitudes.Add(solicitud);
        }

        public void ObtenerAmigoNuevo(Jugador amigo)
        {
            amigos.Add(amigo);
        }

        public void ObtenerNuevoBloqueo(Jugador bloqueado)
        {
            Console.WriteLine("Se está recibiendo el jugador bloqueado.");
            bloqueados.Add(bloqueado);
        }

        public void EnviarEliminacionAmigo(int idJugadorEmisor)
        {
            Console.WriteLine("Se está recibiendo la actualización de la eliminación.");
            Jugador[] arrayAmigos = amigos.ToArray();
            foreach(var a in arrayAmigos)
            {
                if (a.idJugador == idJugadorEmisor)
                {
                    amigos.Remove(a);
                }
            }
        }
    }
}
