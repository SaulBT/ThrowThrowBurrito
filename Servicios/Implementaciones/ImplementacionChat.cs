﻿using Servicios.Interfaces;
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

            string mensajeCompleto = nombreUsuario + " se ha unido!";
            TransmitirMensaje(mensajeCompleto);
        }

        [OperationBehavior]
        public void TransmitirMensaje(string mensajeCompleto)
        {
            foreach (var cliente in clientes)
            {
                cliente.RecibirMensaje(mensajeCompleto);
            }
        }
    }
}
