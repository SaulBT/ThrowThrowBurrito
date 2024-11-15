using Cliente.Logica;
using Cliente.Ventanas.Lobby;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cliente.ServiciosGestionUsuarios;
using Cliente.ServiciosJuego;

namespace Cliente.Ventanas.Lobby
{
    /// <summary>
    /// Interaction logic for Lobby.xaml
    /// </summary>
    public partial class vntLobby : Page
    {
        private ServiciosJuego.Jugador jugador;
        private LogicaChat logicaChat;
        private LogicaJuego logicaJuego;
        private ServiciosJuego.Partida partidaLocal;
        private ServiciosJuego.DatosJugadorPartida[] datosJugadorPartida;
        private ServicioJuegoClient servicioJuegoClient;

        public vntLobby(ServiciosGestionUsuarios.Jugador jugador, ServiciosJuego.Partida partida, LogicaJuego logicaJuego)
        {
            InitializeComponent();
            this.jugador = castJugadorJuego(jugador);
            this.partidaLocal = partida;
            logicaChat= new LogicaChat(this, jugador.nombreUsuario);
            this.logicaJuego = logicaJuego;
            tbcCodigoPartida.Text += partida.codigoPartida;

            InstanceContext contexto = new InstanceContext(this);
            servicioJuegoClient = new ServiciosJuego.ServicioJuegoClient(contexto);
            datosJugadorPartida = logicaJuego.RetornarDatosJugador(partida.codigoPartida);

            tbcPuntosVictoria.Text += partidaLocal.puntajeVictoria;
            tbcTiempoGuerra.Text += partidaLocal.tiempoGuerra;
            ConfigurarVistaAdmin();
            /*
             * hago el retorno de datos directamente en el lobby mientras que el retorno de de partida lo hago desde el menú
             * esto porque ahí mismo se crea la partida (evitando ir a una ventana aparte en caso de ocurra en error) pues 
             * dicho metodo devuelve ya el objeto de la partida, de otro modo tendría que crear la partida en esta misma ventana
             * y en caso de dar error se tendría que estar navegando entre más ventanas
             */


        }

        private ServiciosJuego.Jugador castJugadorJuego(ServiciosGestionUsuarios.Jugador jugadorG)
        {
            ServiciosJuego.Jugador jugadorJ = new ServiciosJuego.Jugador();
            if (jugadorG.descripcion != null)
                jugadorJ.descripcion = jugadorG.descripcion;
            if (jugadorG.fotoPerfil != null)
                jugadorJ.fotoPerfil = jugadorG.fotoPerfil;
            if (jugadorG.estado != null)
                jugadorJ.estado = jugadorG.estado;
            if (jugadorG.esInvitado != null)
                jugadorJ.esInvitado = jugadorG.esInvitado;

            jugadorJ.claveUsuario = jugadorG.claveUsuario;
            jugadorJ.correoElectronico = jugadorG.correoElectronico;
            jugadorJ.contrasenia = jugadorG.contrasenia;
            jugadorJ.idJugador = jugadorG.idJugador;
            jugadorJ.nombreUsuario = jugadorG.nombreUsuario;

            return jugadorJ;
        }

        private ServiciosGestionUsuarios.Jugador castJugadorUsuarios(ServiciosJuego.Jugador jugadorJ)
        {
            ServiciosGestionUsuarios.Jugador jugadorG = new ServiciosGestionUsuarios.Jugador();
            if (jugadorJ.descripcion != null)
                jugadorG.descripcion = jugadorJ.descripcion;
            if (jugadorJ.fotoPerfil != null)
                jugadorG.fotoPerfil = jugadorJ.fotoPerfil;
            if (jugadorJ.estado != null)
                jugadorG.estado = jugadorJ.estado;
            if (jugadorJ.esInvitado != null)
                jugadorG.esInvitado = jugadorJ.esInvitado;

            jugadorG.claveUsuario = jugadorJ.claveUsuario;
            jugadorG.correoElectronico = jugadorJ.correoElectronico;
            jugadorG.contrasenia = jugadorJ.contrasenia;
            jugadorG.idJugador = jugadorJ.idJugador;
            jugadorG.nombreUsuario = jugadorJ.nombreUsuario;

            return jugadorG;
        }

        private void ConfigurarVistaAdmin()
        {
            //Función para configurar las opciones que aparecen en caso de que el cliente sea el admin de la partida

            foreach (ServiciosJuego.DatosJugadorPartida dato in datosJugadorPartida)
            {
                if (dato.claveJugador.Equals(jugador.claveUsuario))
                {
                    if (dato.esAdmin == true)
                    {
                        btnIniciarPartida.Visibility = Visibility.Visible;
                        btnConfigurarPartida.Visibility = Visibility.Visible;
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                
            }
        }

        public void Unirse()
        {
            try
            {
                logicaChat.Unirse(this.jugador.nombreUsuario);
            }
            catch (FaultException ex)
            {
                mostrarAlerta(ex.Message);
            }
            catch (EndpointNotFoundException ex)
            {
                mostrarAlerta("Lo sentimos, no se pudo conectar con el servidor.");
            }
            catch (CommunicationException ex)
            {
                mostrarAlerta("Lo sentimos, la comunicación con el servidor se anuló.");

            }
            catch (Exception ex)
            {
                mostrarAlerta("Lo sentimos, ha ocurrido un error inesperado.");
            }
            
        }

        public void mostrarAlerta(String mensaje)
        {
            gFondoNegro.Visibility = Visibility.Visible;
            gVentanaEmergente.Visibility = Visibility.Visible;
            tbcMensajeEmergente.Text = mensaje;
        }

        private void btnAceptarEmergente_Click(object sender, RoutedEventArgs e)
        {
            gFondoNegro.Visibility = Visibility.Hidden;
            gVentanaEmergente.Visibility = Visibility.Hidden;
            tbcMensajeEmergente.Text = "";
        }

        public void RecibirMensaje(string mensajeCompleto)
        {
            string chatActual = txcChat.Text;
            if (!chatActual.Equals(""))
            {
                txcChat.Text = String.Concat(chatActual, "\n", mensajeCompleto);
            }
            else
            {
                txcChat.Text = mensajeCompleto;
            }
        }
        private void btEnviarMensaje_Click(object sender, RoutedEventArgs e)
        {
            if (!tbcMensaje.Text.Equals(""))
            {
                try
                {
                    string mensaje = tbcMensaje.Text;
                    logicaChat.EnviarMensaje(mensaje);
                    tbcMensaje.Text = "";
                }
                catch (EndpointNotFoundException ex)
                {
                    mostrarAlerta("Lo sentimos, no se pudo conectar con el servidor.");
                }
                catch (CommunicationException ex)
                {
                    mostrarAlerta("Lo sentimos, la comunicación con el servidor se anuló.");

                }
                catch (Exception ex)
                {
                    mostrarAlerta("Lo sentimos, ha ocurrido un error inesperado.");
                }
            }
        }

        public void actualizarChat(string mensaje)
        {
            string chat = txcChat.Text;

            if (!chat.Equals(""))
            {
                txcChat.Text = String.Concat(chat, "\n", mensaje);
                
            }
            else
            {
                txcChat.Text = mensaje;
            }
        }
        
        public void ActualizarPartida(ServiciosJuego.Partida nuevaPartida)
        {
            partidaLocal = nuevaPartida;
            tbcPuntosVictoria.Text = "Puntos para la victoria: " + partidaLocal.puntajeVictoria;
            tbcTiempoGuerra.Text = "Tiempo de guerra: " + partidaLocal.tiempoGuerra;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (ServiciosJuego.DatosJugadorPartida dato in datosJugadorPartida)
                {
                    if (dato.claveJugador.Equals(jugador.claveUsuario))
                    {
                        logicaJuego.SalirPartida(dato, partidaLocal);
                        break;
                    }
                }
                NavigationService.GoBack();
            }
            catch (FaultException ex)
            {
                mostrarAlerta(ex.Message);
            }
            catch (EndpointNotFoundException ex)
            {
                mostrarAlerta("Lo sentimos, no se pudo conectar con el servidor.");
            }
            catch (CommunicationException ex)
            {
                mostrarAlerta("Lo sentimos, la comunicación con el servidor se anuló.");

            }
            catch (Exception ex)
            {
                mostrarAlerta("Lo sentimos, ha ocurrido un error inesperado.");
            }
            
        }

        private void tbcMensaje_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnConfigurarPartida_Click(object sender, RoutedEventArgs e)
        {
            VntConfigurarPartida vntConfigurarPartida = new VntConfigurarPartida(partidaLocal, logicaJuego, this);
            NavigationService.Navigate(vntConfigurarPartida);
        }
    }
}
