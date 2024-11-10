using Cliente.Ventanas.Perfil;
using Cliente.ServiciosGestionUsuarios;
using Cliente.ServiciosJuego;
using Cliente.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.ServiceModel;
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

namespace Cliente.Ventanas
{
    /// <summary>
    /// Interaction logic for MenuPrincipal.xaml
    /// </summary>
    public partial class vntMenuPrincipal : Page
    {
        private SoundPlayer reproductor;
        private ServiciosGestionUsuarios.Jugador jugador;
        private ServicioJuegoClient servicioJuegoClient;
        private LogicaJuego logicaJuego;

        public vntMenuPrincipal(ServiciosGestionUsuarios.Jugador jugador)
        {
            InitializeComponent();
            this.reproductor = new SoundPlayer("Musica/mscMenu.wav");
            this.Loaded += MenuPrincipal_Loaded;
            this.jugador = jugador;
            logicaJuego = new LogicaJuego();
        }

        
        private void MenuPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            this.reproductor.PlayLooping();
        }

        private void mostrarAlerta(String mensaje)
        {
            gFondoNegro.Visibility = Visibility.Visible;
            gVentanaEmergente.Visibility = Visibility.Visible;
            tbcMensajeEmergente.Text = mensaje;
        }

        private void btnAceptarEmergente_Click(object sender, RoutedEventArgs e)
        {
            if (gVentanaUnirsePartida.Visibility != Visibility.Visible)
            {
                gFondoNegro.Visibility = Visibility.Hidden;
            }
            gVentanaEmergente.Visibility = Visibility.Hidden;
            tbcMensajeEmergente.Text = "";
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            this.reproductor.Stop();
            vntLogin vntLogin = new vntLogin();
            NavigationService.Navigate(vntLogin);
        }

        private void btnCrearPartida_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ServiciosJuego.Partida partidaLocal = logicaJuego.CrearPartida(jugador.claveUsuario, jugador.idJugador);

                vntLobby vntLobby = new vntLobby(this.jugador, partidaLocal);
                vntLobby.Unirse();
                NavigationService.Navigate(vntLobby);
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

        private void btnUnirsePartida_Click(object sender, RoutedEventArgs e)
        {
            gFondoNegro.Visibility = Visibility.Visible;
            gVentanaUnirsePartida.Visibility = Visibility.Visible;
        }

        private void btnConfiguraciones_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnVerPerfil_Click(object sender, RoutedEventArgs e)
        {
            VntPerfil vntPerfil = new VntPerfil(jugador);
            NavigationService.Navigate(vntPerfil);
        }

        private void btnAceptarUnirsePartida_Click(object sender, RoutedEventArgs e)
        {
            InstanceContext contexto = new InstanceContext(this);
            servicioJuegoClient = new ServiciosJuego.ServicioJuegoClient(contexto);
            string codigoPartida = tbxCodigoPartida.Text;
            if (!String.IsNullOrEmpty(codigoPartida) )
            {
                try
                {
                    if (logicaJuego.UnirsePartida(codigoPartida, jugador.idJugador, jugador.claveUsuario))
                    {
                        tbcErrorUnirsePartida.Visibility= Visibility.Hidden;
                        ServiciosJuego.Partida partidaLocal = logicaJuego.RetornarPartida(codigoPartida);
                        vntLobby vntLobby = new vntLobby(this.jugador, partidaLocal);
                        vntLobby.Unirse();


                        NavigationService.Navigate(vntLobby);
                    }
                    else
                    {
                        tbcErrorUnirsePartida.Text = "No se encontró una partida existente con ese código";
                        tbcErrorUnirsePartida.Visibility = Visibility.Visible;
                    }
                }
                catch (EndpointNotFoundException ex)
                {
                    mostrarAlerta("Lo sentimos, no se pudo conectar con el servidor.");
                    Console.WriteLine("Error al unirse a una partida: "+ex.Message);
                    servicioJuegoClient.Abort();
                }
                catch (CommunicationException ex)
                {
                    mostrarAlerta("Lo sentimos, la comunicación con el servidor se anuló.");
                    Console.WriteLine("Error al unirse a una partida: " + ex.Message);
                    servicioJuegoClient.Abort();
                }
                catch (Exception ex)
                {
                    mostrarAlerta("Lo sentimos, ha ocurrido un error inesperado.");
                    Console.WriteLine("Error al unirse a una partida: " + ex.Message);
                    servicioJuegoClient.Abort();
                }
            }
            else
            {
                tbcErrorUnirsePartida.Text = "Por favor, no deje el campo vacío";
                tbcErrorUnirsePartida.Visibility = Visibility.Visible;
            }
            
        }

        private void btnCancelarUnirsePartida_Click(object sender, RoutedEventArgs e)
        {
            gVentanaUnirsePartida.Visibility = Visibility.Hidden;
            gFondoNegro.Visibility = Visibility.Hidden;
        }

        private void btnVerAmigos_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
