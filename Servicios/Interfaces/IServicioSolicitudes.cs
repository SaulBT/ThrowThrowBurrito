using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    [ServiceContract(CallbackContract = typeof(IServicioSolicitudesCallback))]
    public interface IServicioSolicitudes
    {
        [OperationContract]
        bool EnviarSolicitud(string claveJugadorRemitente, int idJugador);

        [OperationContract]
        Amigo[] RecibirSolicitudes(int idJugador);

        [OperationContract(IsOneWay = true)]
        void AceptarSolicitud(Amigo solicitud);

        [OperationContract(IsOneWay = true)]
        void RechazarSolicitud(Amigo solicitud);

        [OperationContract(IsOneWay = true)]
        void EnviarInvitacion(string codigoPartida, string correoJugadorInvitado, string nombreUsuarioInvitador);
    }

    [ServiceContract]
    public interface IServicioSolicitudesCallback
    {
        [OperationContract(IsOneWay = true)]
        void ObtenerNuevaSolicitud(SolicitudAmigo nuevaSolicitud);

        [OperationContract(IsOneWay = true)]
        void ActualizarListaAmigos();
    }
}
