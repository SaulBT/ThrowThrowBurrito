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
    internal interface IServicioAmigos
    {
        [OperationContract]
        Jugador[] CargarAmigos(int idJugador);

        [OperationContract]
        void EliminarAmigo(int idJugadorEmisor, int idJugadorReceptor);

        [OperationContract]
        bool BloquearJugador(int idJugadorEmisor, string claveJugadorReceptor);

        [OperationContract]
        void DesbloquearJugador(int idJugadorEmisor, int idJugadorRemitente);

        [OperationContract]
        Jugador[] CargarBloqueados(int idJugadorEmisor);
    }
}
