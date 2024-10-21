using Cliente.ServicioPersonalizarPerfil;
using Cliente.Ventanas.Perfil;
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
    /// Lógica de interacción para MenuPrincipal.xaml
    /// </summary>
    public partial class VntMenuPrincipal : Page
    {
        private String claveUsuario = "FJ7PV3RHTO";//"CMCBTM2YE9";
        //TODO: saul maneja clase Jugador, hay que estandarizar

        public VntMenuPrincipal()
        {
            InitializeComponent();
        }

        public VntMenuPrincipal(String claveUsuario)
        {
            this.claveUsuario = claveUsuario;
            InitializeComponent();
        }

        private void btnRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Ventanas/VntRegistrarUsuario.xaml", UriKind.Relative));
        }

        private void btnVerPerfil_Click(object sender, RoutedEventArgs e)
        {
            VntPerfil verPerfil = new VntPerfil(claveUsuario);
            NavigationService.Navigate(verPerfil);
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

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
