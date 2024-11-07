using System;
using Cliente.ServiciosGestionUsuarios;
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

namespace Cliente.Ventanas.Perfil
{
    /// <summary>
    /// Lógica de interacción para VerPerfil.xaml
    /// </summary>
    public partial class VntPerfil : Page
    {
        private Jugador jugador;

        public VntPerfil(Jugador jugador)
        {
            this.jugador = jugador;
            InitializeComponent();

        }

        private void btnPersonalizarPerfil_Click(object sender, RoutedEventArgs e)
        {
            VntPersonalizarPerfil personalizarPerfil = new VntPersonalizarPerfil(jugador);
            NavigationService.Navigate(personalizarPerfil);
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            vntMenuPrincipal vntMenuPrincipal = new vntMenuPrincipal(jugador);
            NavigationService.Navigate(vntMenuPrincipal);
        }

        private void btnAceptarEmergente_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
