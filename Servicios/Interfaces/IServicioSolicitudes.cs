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
        bool EnviarSolicitud(string claveJugadorEmisor, string claveJugadorRemitente, int idJugador);

        [OperationContract]
        SolicitudAmigo[] RecibirSolicitudes(int idJugador);

        [OperationContract(IsOneWay = true)]
        void AceptarSolicitud(SolicitudAmigo solicitud, int idJugador);

        [OperationContract(IsOneWay = true)]
        void RechazarSolicitud(SolicitudAmigo solicitud);

        [OperationContract(IsOneWay = true)]
        void EnviarInvitacion(string codigoPartida, string correo, string nombreUsuario);
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
