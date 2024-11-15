using Cliente.ServiciosJuego;
using Cliente.Ventanas.Lobby;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Cliente.Logica
{
    public class LogicaChat : IServicioChatCallback
    {
        public ServicioChatClient servicio;
        private vntLobby vntLobby;
        private DispatcherTimer timer;
        bool conexion;
        bool activo;
        private List<String> mensajesPendientes;
        string nombreUsuario;

        public LogicaChat(vntLobby vntLobby, string nombreUsuaio)
        {
            InstanceContext contexto = new InstanceContext(this);
            this.servicio = new ServicioChatClient(contexto);
            this.vntLobby = vntLobby;
            this.conexion = true;
            this.activo = true;
            this.mensajesPendientes = new List<String>();
            this.nombreUsuario = nombreUsuaio;
            configurarTimer();
        }

        public LogicaChat(string nombreUsuario, InstanceContext contexto)
        {
            this.servicio = new ServicioChatClient(contexto);
            this.conexion = true;
            this.activo = true;
            this.mensajesPendientes = new List<String>();
            this.nombreUsuario = nombreUsuario;
            configurarTimer();
        }

        private void checarCanal()
        {
            if (servicio.State == CommunicationState.Faulted)
            {
                InstanceContext contexto = new InstanceContext(this);
                servicio = new ServicioChatClient(contexto);
            }
        }
        private void configurarTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                checarCanal();
                conexion = servicio.ProbarConexion();
                if (conexion && !activo)
                {
                    Unirse(nombreUsuario);
                    activo = true;
                    enviarMensajesPendientes();
                }
            }
            catch (EndpointNotFoundException ex)
            {
                if (conexion)
                {
                    conexion = false;
                    activo = false;
                    vntLobby.mostrarAlerta("Lo sentimos, no se pudo conectar con el servidor.");
                }
            }
            catch (CommunicationException ex)
            {
                if (conexion)
                {
                    conexion = false;
                    activo = false;
                    vntLobby.mostrarAlerta("Lo sentimos, la comunicación con el servidor se anuló.");
                }
            }
            catch (Exception ex)
            {
                if (conexion)
                {
                    conexion = false;
                    activo = false;
                    vntLobby.mostrarAlerta("Lo sentimos, ha ocurrido un error inesperado.");
                }
            }
        }

        public void Unirse(String nombreUsuario)
        {
            if (servicio.State != CommunicationState.Opened)
            {
                servicio.Open();
            }

            servicio.Unirse(nombreUsuario);
        }

        public void EnviarMensaje(string mensaje)
        {
            if (conexion)
            {
                servicio.EnviarMensaje(nombreUsuario, mensaje);
            }
            else
            {
                mensajesPendientes.Add(mensaje);
            }
        }

        private void enviarMensajesPendientes()
        {
            foreach (string mensajePendiente in mensajesPendientes)
            {
                servicio.EnviarMensaje(nombreUsuario, mensajePendiente);
            }
            mensajesPendientes.Clear();
        }

        public void RecibirMensaje(string mensajeCompleto)
        {
            vntLobby.actualizarChat(mensajeCompleto);
        }
    }
}
