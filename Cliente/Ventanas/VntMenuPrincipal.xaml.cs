using Cliente.ServicioLogin;
using Cliente.Ventanas.Perfil;
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
        private Jugador jugador;

        public vntMenuPrincipal(Jugador jugador)
        {
            InitializeComponent();
            this.reproductor = new SoundPlayer("Musica/mscMenu.wav");
            this.Loaded += MenuPrincipal_Loaded;
            this.jugador = jugador;
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
            gFondoNegro.Visibility = Visibility.Hidden;
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
            NavigationService.Navigate(new vntLobby(this.jugador));
        }

        private void btnUnirsePartida_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnConfiguraciones_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnVerPerfil_Click(object sender, RoutedEventArgs e)
        {
            VntPerfil vntPerfil = new VntPerfil(jugador);
            NavigationService.Navigate(vntPerfil);
        }
    }
}
