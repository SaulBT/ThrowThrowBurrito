using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    [ServiceContract]
    public interface IServicioSolicitudes
    {
        [OperationContract]
        bool EnviarSolicitud(string claveJugadorEmisor, string claveJugadorRemitente, int idJugador);

        [OperationContract]
        SolicitudAmigo[] RecibirSolicitudes(string claveJugador);

        [OperationContract]
        void AceptarSolicitud(SolicitudAmigo solicitud);

        [OperationContract]
        void RechazarSolicitud(int idSolicitudAmigo);

        [OperationContract]
        void EnviarInvitacion(string codigoPartida, string codigoJugadorInvitado);
    }
}
