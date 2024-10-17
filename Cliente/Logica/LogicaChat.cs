using Cliente.ServicioChat;
using Cliente.ServicioLogin;
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
        MainWindow ventana;

        public LogicaChat(MainWindow ventana)
        {
            InstanceContext contexto = new InstanceContext(this);
            servicio = new ServicioChat.ServicioChatClient(contexto);
            this.ventana = ventana;
        }

        public ServicioChat.ServicioChatClient Servicio
        {
            get { return servicio; }
        }

        public void RecibirMensaje(string mensajeCompleto)
        {
            ventana.actualizarChat(mensajeCompleto);
        }

        public void Unirse(String nombreUsuario)
        {
            servicio.Unirse(nombreUsuario);
        }
    }
}
