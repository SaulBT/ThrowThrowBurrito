using Cliente.ServicioPersonalizarPerfil;
using Cliente.ServicioRegistrarUsuario;
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

namespace Cliente
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        const string claveUsuario = "FJ7PV3RHTO";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            VentanaRegistroUsuario ventanaRegistroUsuario = new VentanaRegistroUsuario();
            ventanaRegistroUsuario.Show();
            this.Close();
        }

        private void btnPersonalizarPerfil_Click(object sender, RoutedEventArgs e)
        {
            

            VentanaPersonalizarPerfil ventanaPersonalizarPerfil = new VentanaPersonalizarPerfil(claveUsuario);
            ventanaPersonalizarPerfil.Show();
            this.Close();
        }
    }
}
