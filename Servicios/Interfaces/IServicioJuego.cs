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
        bool UnirsePartida(string codigoPartida, int idJugador, string claveJugador);

        [OperationContract(IsOneWay = true)]
        void CambiarConfiguracionPartida(Partida partidaLocal);

        [OperationContract(IsOneWay = true)]
        void CambiarDatosJugador(DatosJugadorPartida datosLocales);

        [OperationContract]
        DatosJugadorPartida[] RetornarDatosJugador(string codigoPartida);

        [OperationContract]
        Partida RetornarPartida(string codigoPartida);

        [OperationContract(IsOneWay = true)]
        void IniciarPartida(string codigoPartida);

        [OperationContract(IsOneWay = true)]
        void SalirPartida(DatosJugadorPartida datos, Partida partidaLocal);
    }

    [ServiceContract]
    public interface IServicioJuegoCallback
    {
        [OperationContract]
        void ActualizarPartida(Partida partidaLocal);

        [OperationContract]
        void ActualizarDatos(DatosJugadorPartida datos);
    }
}
