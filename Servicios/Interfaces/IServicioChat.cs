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
        [OperationContract(IsOneWay = true)]
        void Unirse(string nombreUsuario);

        [OperationContract(IsOneWay = true)]
        void EnviarMensaje(string nombreUsuario, string mensaje);

        [OperationContract]
        bool ProbarConexion();

        [OperationContract(IsOneWay = true)]
        void Salir(string nombreUsuario);
    }

    [ServiceContract]
    public interface IServicioChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void RecibirMensaje(string mensajeCompleto);
    }
}
