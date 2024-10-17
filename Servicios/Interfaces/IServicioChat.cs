using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    [ServiceContract(CallbackContract = typeof(IServicioChatCallback))]
    public interface IServicioChat
    {
        [OperationContract]
        void Unirse(string nombreUsuario);

        [OperationContract]
        void enviarMensaje(string nombreUsuario, string mensaje);
    }

    [ServiceContract]
    public interface IServicioChatCallback
    {
        [OperationContract]
        void RecibirMensaje(string mensajeCompleto);
    }
}
