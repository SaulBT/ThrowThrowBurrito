using Cliente.Logica;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private LogicaLogin logica;
        public Login()
        {
            InitializeComponent();
            logica = new LogicaLogin();
        }

        private void btnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            bool verificacion = verificarCampos();
            if (verificacion)
            {
                string nombreUsuario = tbxNombreUsuario.Text;
                string contrasenia = tbxContrasenia.Text;
                Jugador jugador = logica.IniciarSesion(nombreUsuario, contrasenia);
                if (verificarJugador(jugador))
                {
                    irAMenu(jugador);
                } else
                {
                    mostrarAlerta("No se encontró al usuario, por favor revisa tu Nombre de Usuario o tu Contraseña");
                }
            }
        }

        private bool verificarCampos()
        {
            tbcErrorNombreUsuario.Text = "";
            tbcErrorContrasenia.Text = "";
            bool resultado = true;
            if (tbxNombreUsuario.Text.Equals(""))
            {
                tbcErrorNombreUsuario.Text = "Por favor, llena este campo";
                resultado = false;
            }

            if (tbxContrasenia.Text.Equals(""))
            {
                tbcErrorContrasenia.Text = "Por favor, llena este campo";
                resultado = false;
            }

            return resultado;
        }

        private bool verificarJugador(Jugador jugador)
        {
            if (jugador != null)
            {
                return true;
            } else
            {
                return false;
            }
        }

        private void irAMenu(Jugador jugador)
        {
            NavigationService.Navigate(new Uri("Ventanas/MenuPrincipal.xaml", UriKind.Relative), jugador);
        }

        private void mostrarAlerta(string mensaje)
        {
            gFondoNegro.Visibility = Visibility.Visible;
            gVentanaEmergente.Visibility = Visibility.Visible;
            tbcMensajeEmergente.Text = mensaje;
        }

        private void btnAceptarEmergente_Click(object sender, RoutedEventArgs e)
        {
            esconderAlerta();
        }

        private void esconderAlerta()
        {
            gFondoNegro.Visibility = Visibility.Hidden;
            gVentanaEmergente.Visibility = Visibility.Hidden;
            tbcMensajeEmergente.Text = "";
        }

        private void btnEntrarInvitado_Click(object sender, RoutedEventArgs e)
        {
            mostrarAlerta("¡Vaya! Parece que esta página aún no está terminada.");
        }

        private void btnCrearCuenta_Click(object sender, RoutedEventArgs e)
        {
            mostrarAlerta("¡Vaya! Parece que esta página aún no está terminada.");
            //Aqui va lo de Pablo
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
