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
        int EnviarSolicitud(string claveJugadorRemitente, int idJugador);

        [OperationContract]
        Jugador[] RecibirSolicitudes(int idJugador);

        [OperationContract(IsOneWay = true)]
        void AceptarSolicitud(int idJugadorEmisor, int idJugadorReceptor);

        [OperationContract(IsOneWay = true)]
        void RechazarSolicitud(int idJugadorEmisor, int idJugadorReceptor);

        [OperationContract(IsOneWay = true)]
        void EnviarInvitacion(string codigoPartida, string correoJugadorInvitado, string nombreUsuarioInvitador);

        [OperationContract(IsOneWay = true)]
        void EnviarCliente(int idJugador);
    }

    [ServiceContract]
    public interface IServicioSolicitudesCallback
    {
        [OperationContract]
        void ObtenerNuevaSolicitud(Jugador solicitud);

        [OperationContract]
        void ObtenerAmigoNuevo(Jugador amigo);

        [OperationContract]
        void EnviarEliminacionAmigo(int idJugadorEmisor);

        [OperationContract]
        void ObtenerNuevoBloqueo(Jugador bloqueado);
    }
}
