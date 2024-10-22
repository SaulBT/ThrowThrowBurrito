using Cliente.ServicioLogin;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class MenuPrincipal : Page
    {
        private Jugador jugador;

        public MenuPrincipal()
        {
            InitializeComponent();
            this.Loaded += MenuPrincipal_Loaded;
        }

        private void MenuPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            if (navigationService != null)
            {
                navigationService.LoadCompleted += NavigationService_LoadCompleted;

            }
        }

        private void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData is Jugador jugador)
            {
                this.jugador = e.ExtraData as Jugador;
            }
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Ventanas/Login.xaml", UriKind.Relative));
        }

        private void btnCrearPartida_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnUnirsePartida_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnConfiguraciones_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnVerPerfil_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
