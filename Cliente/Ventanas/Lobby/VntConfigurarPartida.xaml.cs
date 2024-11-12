using Cliente.Logica;
using Cliente.ServiciosJuego;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cliente.Ventanas.Lobby
{
    /// <summary>
    /// Lógica de interacción para VntConfigurarPartida.xaml
    /// </summary>
    public partial class VntConfigurarPartida : Page
    {
        private Partida partida;
        private ServiciosJuego.ServicioJuegoClient servicio;
        private bool operacionFueExitosa = false;
        private LogicaJuego logica;
        private vntLobby vntLobby;


        public VntConfigurarPartida(Partida partidaLocal, LogicaJuego logica, vntLobby vntLobby)
        {
            InitializeComponent();
            partida = partidaLocal;
            InstanceContext contexto = new InstanceContext(this);
            servicio = new ServicioJuegoClient(contexto);
            this.logica = logica;
            this.vntLobby = vntLobby;
            CargarConfiguraciones();
        }

        private void CargarConfiguraciones()
        {
            if (sldPuntos != null && sldTiempoGuerra != null)
            {
                sldPuntos.Value = partida.puntajeVictoria.HasValue ? (double)partida.puntajeVictoria.Value : 0.0;
                sldTiempoGuerra.Value = partida.tiempoGuerra.HasValue ? (double)partida.tiempoGuerra.Value : 0.0;
            }
        }

        private void sldTiempoGuerra_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (tbcValorTiempo!= null && sldTiempoGuerra!= null)
                tbcValorTiempo.Text = sldTiempoGuerra.Value.ToString();
        }

        private void sldPuntos_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (tbcValorPuntos != null && sldPuntos != null)
                tbcValorPuntos.Text = sldPuntos.Value.ToString();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                partida.tiempoGuerra = (int)sldTiempoGuerra.Value;
                partida.puntajeVictoria = (int)sldPuntos.Value;
                logica.CambiarConfiguracionPartida(partida);
                operacionFueExitosa = true;
                mostrarAlerta("Los cambios se realizaron correctamente.");
            }
            catch (EndpointNotFoundException ex)
            {
                mostrarAlerta("Lo sentimos, no se pudo conectar con el servidor.");
                Console.WriteLine(ex.Message);
                servicio.Abort();
            }
            catch (CommunicationException ex)
            {
                mostrarAlerta("Lo sentimos, la comunicación con el servidor se anuló.");
                Console.WriteLine(ex.Message);
                servicio.Abort();
            }
            catch (Exception ex)
            {
                mostrarAlerta("Lo sentimos, ha ocurrido un error inesperado.");
                Console.WriteLine(ex.Message);
                servicio.Abort();
            }
        }

        private void btnAceptarEmergente_Click(object sender, RoutedEventArgs e)
        {
            gVentanaEmergente.Visibility = Visibility.Hidden;
            tbcMensajeEmergente.Text = "";
            if (operacionFueExitosa)
            {
                NavigationService.GoBack();
            }
        }

        private void mostrarAlerta(string mensaje)
        {
            gFondoNegro.Visibility = Visibility.Visible;
            gVentanaEmergente.Visibility = Visibility.Visible;
            tbcMensajeEmergente.Text = mensaje;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
