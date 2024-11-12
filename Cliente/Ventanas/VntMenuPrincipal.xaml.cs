using Cliente.Ventanas.Perfil;
using Cliente.ServiciosJuego;
using Cliente.Logica;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
namespace Cliente.Ventanas
{
    /// <summary>
    /// Interaction logic for MenuPrincipal.xaml
    /// </summary>
    public partial class vntMenuPrincipal : Page
    {
        private MediaPlayer reproductorMusica = new MediaPlayer();
        private ServiciosGestionUsuarios.Jugador jugador;
        private ServicioJuegoClient servicioJuegoClient;
        private LogicaJuego logicaJuego;

        public vntMenuPrincipal(ServiciosGestionUsuarios.Jugador jugador)
        {
            if (System.IO.File.Exists(ConfiguracionGeneral.nombreArchivoConfiguraciones))
            {
                ConfiguracionGeneral.cargarConfiguraciones();
            }
            InitializeComponent();
            this.Loaded += MenuPrincipal_Loaded;
            this.jugador = jugador;
            logicaJuego = new LogicaJuego();
        }


        private void MenuPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            if (reproductorMusica.Position != TimeSpan.Zero && reproductorMusica.Position < reproductorMusica.NaturalDuration.TimeSpan)
            {
                actualizarMusica();
            }
            else
            {
                configurarMusica();
            }
        }

        private void actualizarMusica()
        {
            reproductorMusica.Volume = ConfiguracionGeneral.VolumenGeneral * ConfiguracionGeneral.Musica;
        }

        private void configurarMusica()
        { 
            reproductorMusica.Open(new Uri("Musica/mscMenu.wav", UriKind.Relative));
            reproductorMusica.Volume = ConfiguracionGeneral.VolumenGeneral * ConfiguracionGeneral.Musica;
            reproductorMusica.MediaEnded += (s, ev) => reproductorMusica.Position = TimeSpan.Zero;
            reproductorMusica.Play();
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
            reproductorMusica.Stop();
            vntLogin vntLogin = new vntLogin();
            NavigationService.Navigate(vntLogin);
        }

        private void btnCrearPartida_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ServiciosJuego.Partida partidaLocal = logicaJuego.CrearPartida(jugador.claveUsuario, jugador.idJugador);

                vntLobby vntLobby = new vntLobby(this.jugador, partidaLocal, logicaJuego);
                logicaJuego.AsignarVentanaLobby(vntLobby);
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
            VntConfiguracion vntConfiguracion = new VntConfiguracion();
            NavigationService.Navigate(vntConfiguracion);
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
                    ServiciosJuego.Partida partidaLocal = logicaJuego.UnirsePartida(codigoPartida, jugador.idJugador, jugador.claveUsuario);
                    if (partidaLocal != null)
                    {
                        tbcErrorUnirsePartida.Visibility= Visibility.Hidden;
                        //ServiciosJuego.Partida partidaLocal = logicaJuego.RetornarPartida(codigoPartida);
                        
                        vntLobby vntLobby = new vntLobby(this.jugador, partidaLocal, logicaJuego);
                        logicaJuego.AsignarVentanaLobby(vntLobby);
                        vntLobby.Unirse();


                        NavigationService.Navigate(vntLobby);
                    }
                    else
                    {
                        tbcErrorUnirsePartida.Text = "No se encontró una partida existente o disponible con ese código";
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
    }
}
