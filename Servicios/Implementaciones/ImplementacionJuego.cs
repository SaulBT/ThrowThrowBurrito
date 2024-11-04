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
            } while (!ValidarCodigoNoRepetido(partidaLocal.codigoPartida));
            /*
            
            */
            CrearDatosJugadorPartida(true, claveJugador, partidaLocal.codigoPartida, idJugador);

            var clienteJuego = OperationContext.Current.GetCallbackChannel<IServicioJuegoCallback>();
            clientesJuego.Add(claveJugador, clienteJuego);
            partidasActuales.Add(partidaLocal);

            return partidaLocal;
        }

        public bool ValidarCodigoNoRepetido(string clave)
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
                codigoPartida = clavePartida,
                idJugador = idJugador,
            };

            datosActuales.Add(datosJugadorPartida);
        }

        public bool UnirsePartida(string codigoPartida, int idJugador, string claveJugador)
        {
            foreach(var p in partidasActuales)
            {
                if(p.codigoPartida.Equals(codigoPartida))
                {
                    CrearDatosJugadorPartida(false, claveJugador, codigoPartida, idJugador);
                    return true;
                }
            }

            return false;
        }

        public DatosJugadorPartida[] RetornarDatosJugador(string codigoPartida)
        {
            int j = 0;
            DatosJugadorPartida[] datos = new DatosJugadorPartida[4];
            foreach(var d in datosActuales)
            {
                if (d.codigoPartida == codigoPartida)
                {
                    datos[j] = d;
                    j++;
                }
            }

            return datos;
        }

        public Partida RetornarPartida(string codigoPartida)
        {
            Partida retorno = new Partida();

            foreach(var p in partidasActuales)
            {
                if(p.codigoPartida == codigoPartida)
                {
                    retorno = p;
                }
            }

            return retorno;
        }

        public void CambiarConfiguracionPartida(Partida partidaLocal)
        {
            Partida partida = partidasActuales.Find(p => p.idPartida == partidaLocal.idPartida);
            if (partida == null)
            {
                partida = partidaLocal;
            }

            foreach(var d in datosActuales)
            {
                if(d.codigoPartida == partidaLocal.codigoPartida)
                {
                    var cliente = clientesJuego[d.claveJugador];
                    cliente.ActualizarPartida(partidaLocal);
                }
            }
        }

        public void CambiarDatosJugador(DatosJugadorPartida datosLocales)
        {
            DatosJugadorPartida datos = datosActuales.Find(d => d.claveJugador == datosLocales.claveJugador);
            if (datos == null)
            {
                datos = datosLocales;
            }

            foreach (var d in datosActuales)
            {
                if (d.codigoPartida == datosLocales.codigoPartida)
                {
                    var cliente = clientesJuego[d.claveJugador];
                    cliente.ActualizarDatos(datosLocales);
                }
            }
        }

        public void IniciarPartida(string codigoPartida)
        {
            Partida partida = partidasActuales.Find(p => p.codigoPartida == codigoPartida);
            using (var contexto = new ModeloDBContainer())
            {
                contexto.Database.Log = Console.WriteLine;
                var partidaN = new Partida
                {
                    codigoPartida = partida.codigoPartida,
                    fecha = partida.fecha,
                    tiempoGuerra = partida.tiempoGuerra,
                    puntajeVictoria = partida.puntajeVictoria,
                    duracion = partida.duracion,
                    nombreGanador = partida.nombreGanador,
                };
                contexto.Partida.Add(partida);
                contexto.SaveChanges();

                partida.idPartida = (from p in contexto.Partida
                                     where p.codigoPartida ==  partida.codigoPartida select p.idPartida).FirstOrDefault();

                foreach(var d in datosActuales)
                {
                    if (d.codigoPartida == partida.codigoPartida)
                    {
                        var datos = new DatosJugadorPartida
                        {
                            puntaje = d.puntaje,
                            idAspecto = d.idAspecto,
                            expulsado = d.expulsado,
                            idJugador = d.idJugador,
                            idPartida = partida.idPartida,
                            esAdmin = d.esAdmin,
                            claveJugador = d.claveJugador,
                            codigoPartida = d.codigoPartida
                        };
                        contexto.DatosJugadorPartida.Add(datos);
                        contexto.SaveChanges();

                    }
                }
            }
        }

        public void SalirPartida(DatosJugadorPartida datos, Partida partidaLocal)
        {
            //datosActuales.Remove(datos);
            var comparadorDatos = new ComparadorDatosJugadorPartida(); 
            datosActuales = datosActuales.Except(new List<DatosJugadorPartida> { datos }, comparadorDatos).ToList();

            if (datos.esAdmin == true)
            {
                if (!cambiarAdmin(datos.codigoPartida))
                {
                    //partidasActuales.Remove(partidaLocal);
                    var comparadorPartidas = new ComparadorPartida();
                    partidasActuales = partidasActuales.Except(new List<Partida> {partidaLocal}, comparadorPartidas).ToList();  
                }
            }
            clientesJuego.Remove(datos.claveJugador);
        }

        private bool cambiarAdmin(String codigoPartida)
        {
            foreach(var d in datosActuales)
            {
                if (d == null)
                {
                    break;
                }
                else 
                {
                    d.esAdmin = true;
                    return true;
                }
            }
            return false;
        }

        private class ComparadorDatosJugadorPartida : IEqualityComparer<DatosJugadorPartida>
        {
            public bool Equals(DatosJugadorPartida x, DatosJugadorPartida y)
            {
                if (Object.ReferenceEquals(x, y)) 
                    return true; 
                if (x == null || y == null) 
                    return false; 
                return x.claveJugador == y.claveJugador && x.codigoPartida == y.codigoPartida && x.idJugador == y.idJugador;
            }

            public int GetHashCode(DatosJugadorPartida d)
            {
                if (d == null) 
                    return 0; 
                int hashClaveJugador = d.claveJugador == null ? 0 : d.claveJugador.GetHashCode(); 
                int hashCodigoPartida = d.codigoPartida == null ? 0 : d.codigoPartida.GetHashCode(); 
                int hashIdJugador = d.idJugador.GetHashCode(); 
                return hashClaveJugador ^ hashCodigoPartida ^ hashIdJugador;
            }
        }
        private class ComparadorPartida : IEqualityComparer<Partida>
        {
            public bool Equals(Partida x, Partida y)
            {
                if (Object.ReferenceEquals (x, y)) 
                    return true;
                if (x == null || y == null) 
                    return false;
                return x.idPartida == y.idPartida && x.codigoPartida == y.codigoPartida;
            }

            public int GetHashCode(Partida p)
            {
                if (p == null)
                    return 0;
                int hashIdPartida = p.idPartida.GetHashCode();
                int hashCodigoPartida = p.codigoPartida == null ? 0 : p.codigoPartida.GetHashCode();
                return hashCodigoPartida ^ hashIdPartida;
            }
        }
    }
}
