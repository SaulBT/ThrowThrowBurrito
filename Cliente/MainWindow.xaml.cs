using Cliente.ServicioLogin;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Cliente
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void tbUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbContrasenia_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {

            ServicioLogin.ServicioLoginClient clienteLogin = new ServicioLogin.ServicioLoginClient();
            string nombreUsuario = tbUsuario.Text;
            string contrasenia = tbContrasenia.Text;

            try
            {
                Jugador jugador = clienteLogin.Login(nombreUsuario, contrasenia);
                if (jugador != null)
                {
                    tbPrueba.Content = "Exito!";
                }
                else
                {
                    tbPrueba.Content = "No hay jugador.";
                }
            }
            catch (FaultException ex)
            {
                tbPrueba.Content += ex.Message;
            }
        }
    }
}
