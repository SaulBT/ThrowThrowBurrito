using Cliente.ServicioChat;
using Cliente.ServicioLogin;
using Cliente.Ventanas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Logica
{
    public class LogicaChat : IServicioChatCallback
    {
        private ServicioChat.ServicioChatClient servicio;
        private vntLobby vntLobby;

        public LogicaChat(vntLobby vntLobby)
        {
            InstanceContext contexto = new InstanceContext(this);
            this.servicio = new ServicioChat.ServicioChatClient(contexto);
            this.vntLobby = vntLobby;
        }

        public ServicioChat.ServicioChatClient Servicio
        {
            get { return servicio; }
        }

        public void Unirse(String nombreUsuario)
        {
            servicio.Unirse(nombreUsuario);
        }

        public void EnviarMensaje(string nombreUsuario, string mensaje)
        {
            servicio.EnviarMensaje(nombreUsuario, mensaje);
        }

        public void RecibirMensaje(string mensajeCompleto)
        {
            vntLobby.actualizarChat(mensajeCompleto);
        }
    }
}
