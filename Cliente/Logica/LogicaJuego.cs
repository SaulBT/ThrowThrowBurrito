using Cliente.ServicioJuego;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Logica
{
    internal class LogicaJuego : IServicioJuegoCallback
    {
        private ServicioJuegoClient servicio;

        public LogicaJuego()
        {
            InstanceContext contexto = new InstanceContext(this);
            servicio = new ServicioJuegoClient(contexto);
        }
        public void ActualizarDatos(DatosJugadorPartida datos)
        {
            throw new NotImplementedException();
        }

        public void ActualizarPartida(Partida partidaLocal)
        {
            throw new NotImplementedException();
        }

        public bool UnirsePartida(string codigoPartida, int idJugador, string claveUsuario)
        {
            return servicio.UnirsePartida(codigoPartida, idJugador, claveUsuario);
        }

        public Partida CrearPartida(string claveUsuario, int idJugador)
        {
            InstanceContext contexto = new InstanceContext(this);
            servicio = new ServicioJuegoClient(contexto);
            var partida = servicio.CrearPartida(claveUsuario, idJugador);
            Partida partidaLocal = new Partida
            {
                fecha = partida.fecha,
                tiempoGuerra = partida.tiempoGuerra,
                puntajeVictoria = partida.puntajeVictoria,
                duracion = partida.duracion,
                estado = partida.estado,
                nombreGanador = partida.nombreGanador,
                codigoPartida = partida.codigoPartida,
            };
            Console.WriteLine("Nueva partida creada con codigo: " + partidaLocal.codigoPartida);
            return partidaLocal;
        }

        public Partida RetornarPartida(string codigoPartida)
        {
            return servicio.RetornarPartida(codigoPartida);
        }

        public DatosJugadorPartida[] RetornarDatosJugador(string codigoPartida)
        {
            return servicio.RetornarDatosJugador(codigoPartida);
        }

        public void SalirPartida(DatosJugadorPartida datos, Partida partidaLocal)
        {
            servicio.SalirPartida(datos, partidaLocal);
        }
    }
}
