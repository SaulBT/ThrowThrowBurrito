using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Implementaciones
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class ImplementacionChat : IServicioChat
    {
        private static List<IServicioChatCallback> clientes = new List<IServicioChatCallback>();
        public void EnviarMensaje(string nombreUsuario, string mensaje)
        {
            string mensajeCompleto = "<" + nombreUsuario + ">: " + mensaje;
            TransmitirMensaje(mensajeCompleto);
        }

        public void Unirse(string nombreUsuario)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IServicioChatCallback>();
            if (!clientes.Contains(callback))
            {
                clientes.Add(callback);
            }
            Console.WriteLine("Llego " + nombreUsuario);
            string mensajeCompleto = nombreUsuario + " se ha unido!";
            TransmitirMensaje(mensajeCompleto);
        }

        public void Salir(string nombreUsuario)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IServicioChatCallback>();
            clientes.Remove(callback);
            string mensajeCompleto = nombreUsuario + " se ha ido!";
            TransmitirMensaje(mensajeCompleto);
        }

        public bool ProbarConexion()
        {
            return true;
        }

        [OperationBehavior]
        public void TransmitirMensaje(string mensajeCompleto)
        {
            foreach (var cliente in clientes)
            {
                Console.WriteLine("Se esta transmitiendo el mensaje");
                cliente.RecibirMensaje(mensajeCompleto);
            }
        }
    }
}
