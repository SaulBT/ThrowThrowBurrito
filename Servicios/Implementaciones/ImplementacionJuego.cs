using AccesoDatos;
using NUnit.Framework.Constraints;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Implementaciones
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class ImplementacionJuego : IServicioChat, IServicioJuego
    {
        const int TIEMPO_GUERRA_DEFAULT = 20;
        const int PUNTOS_VICTORIA_DEFAULT = 10;
        const int LONGITUD_CODIGO_PARTIDA = 10;
        private static List<IServicioChatCallback> clientes = new List<IServicioChatCallback>();
        private static Dictionary<String,IServicioJuegoCallback> clientesJuego = new Dictionary<String,IServicioJuegoCallback>();
        private static List<Partida> partidasActuales = new List<Partida>();
        private static List<DatosJugadorPartida> datosActuales = new List<DatosJugadorPartida>();
        
        public void EnviarMensaje(string nombreUsuario, string mensaje)
        {
            string mensajeCompleto = "<" + nombreUsuario + ">: " + mensaje;
            TransmitirMensaje(mensajeCompleto);
        }

        public void Unirse(string nombreUsuario)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IServicioChatCallback>();
            if (!clientes.Contains(callback))
            {
                clientes.Add(callback);
            }
            string mensajeCompleto = nombreUsuario + " se ha unido!";
            TransmitirMensaje(mensajeCompleto);
        }

        public void Salir(string nombreUsuario)
        {
            var callback = OperationContext.Current.GetCallbackChannel<IServicioChatCallback>();
            clientes.Remove(callback);
            string mensajeCompleto = nombreUsuario + " se ha ido!";
            TransmitirMensaje(mensajeCompleto);
        }

        public bool ProbarConexion()
        {
            return true;
        }

        [OperationBehavior]
        public void TransmitirMensaje(string mensajeCompleto)
        {
            foreach (var cliente in clientes)
            {
                cliente.RecibirMensaje(mensajeCompleto);
            }
        }
        //Juego
        public Partida CrearPartida(string claveJugador, int idJugador)
        {
            /*
            string formato = "yyyy-MM-dd";
            DateTime fechaActual = DateTime.Now;
            string fechaActualS = fechaActual.ToString(formato);*/

            Partida partidaLocal = new Partida
            {
                fecha = DateTime.Now,
                tiempoGuerra = TIEMPO_GUERRA_DEFAULT,
                puntajeVictoria = PUNTOS_VICTORIA_DEFAULT,
                duracion = null,
                estado = "En espera",
                nombreGanador = null,
            };

            do
            {
                partidaLocal.codigoPartida = Utilidades.GenerarCodigo(LONGITUD_CODIGO_PARTIDA);
            } while (!ValidarClaveNoRepetida(partidaLocal.codigoPartida));
            /*
            using (var contexto = new ModeloDBContainer())
            {
                contexto.Database.Log = Console.WriteLine;
                var partida = new Partida
                {
                    codigoPartida = partidaLocal.codigoPartida,
                    fecha = partidaLocal.fecha,
                    tiempoGuerra = partidaLocal.tiempoGuerra,
                    puntajeVictoria = partidaLocal.puntajeVictoria,
                    duracion = partidaLocal.duracion,
                    nombreGanador = partidaLocal.nombreGanador,
                };
                contexto.Partida.Add(partida);
                contexto.SaveChanges();

                partidaLocal.idPartida = (from p in contexto.Partida
                               where p.codigoPartida == partidaLocal.codigoPartida
                               select p.idPartida).FirstOrDefault();
            }
            */
            CrearDatosJugadorPartida(true, claveJugador, partidaLocal.codigoPartida, idJugador);

            var clienteJuego = OperationContext.Current.GetCallbackChannel<IServicioJuegoCallback>();
            clientesJuego.Add(claveJugador, clienteJuego);

            return partidaLocal;
        }

        public bool ValidarClaveNoRepetida(string clave)
        {
            using (var contexto = new ModeloDBContainer())
            {
                contexto.Database.Log = Console.WriteLine;
                var partida = (from p in contexto.Partida
                               where p.codigoPartida == clave
                               select p).FirstOrDefault();

                if (partida == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public void CrearDatosJugadorPartida(bool esAdmin, string claveJugador, string clavePartida, int idJugador)
        {
            DatosJugadorPartida datosJugadorPartida = new DatosJugadorPartida
            {
                puntaje = 0,
                idAspecto = null,
                expulsado = false,
                esAdmin = esAdmin,
                claveJugador = claveJugador,
                clavePartida = clavePartida,
                idJugador = idJugador,
            };

            datosActuales.Add(datosJugadorPartida);
        }

        public bool UnirsePartida(string clavePartida, int idJugador, string claveJugador)
        {
            foreach(var p in partidasActuales)
            {
                if(p.codigoPartida == clavePartida)
                {
                    CrearDatosJugadorPartida(false, claveJugador, clavePartida, idJugador);
                    return true;
                }
            }

            return false;
        }

        public DatosJugadorPartida[] RetornarDatosJugador(string clavePartida)
        {
            int j = 0;
            DatosJugadorPartida[] datos = new DatosJugadorPartida[4];
            foreach(var d in datosActuales)
            {
                if (d.clavePartida == clavePartida)
                {
                    datos[j] = d;
                    j++;
                }
            }

            return datos;
        }

        public void CambiarConfiguracionPartida()
        {
            throw new NotImplementedException();
        }
    }
}
