using Cliente.ServicioPersonalizarPerfil;
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

namespace Cliente.Ventanas.Perfil
{
    /// <summary>
    /// Lógica de interacción para VerPerfil.xaml
    /// </summary>
    public partial class VntPerfil : Page
    {
        private string claveUsuario;

        public VntPerfil(String claveUsuario)
        {
            this.claveUsuario = claveUsuario;
            InitializeComponent();

        }

        private void btnPersonalizarPerfil_Click(object sender, RoutedEventArgs e)
        {
            VntPersonalizarPerfil personalizarPerfil = new VntPersonalizarPerfil(claveUsuario);
            NavigationService.Navigate(personalizarPerfil);
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Ventanas/VntMenuPrincipal.xaml", UriKind.Relative), claveUsuario);
        }

        private void btnAceptarEmergente_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
