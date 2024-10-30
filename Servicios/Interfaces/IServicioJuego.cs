using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Interfaces
{
    [ServiceContract(CallbackContract = typeof(IServicioJuegoCallback))]
    public interface IServicioJuego
    {
        [OperationContract]
        Partida CrearPartida(string claveJugador, int idJugador);

        [OperationContract]
        bool UnirsePartida(string clavePartida, int idJugador, string claveJugador);

        [OperationContract(IsOneWay = true)]
        void CambiarConfiguracionPartida();

        [OperationContract]
        DatosJugadorPartida[] RetornarDatosJugador(string clavePartida);
    }

    [ServiceContract]
    public interface IServicioJuegoCallback
    {
        [OperationContract]
        Partida ActualizarPartida();
    }
}
