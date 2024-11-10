using Cliente.Logica;
using Cliente.ServiciosGestionUsuarios;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Cliente.Ventanas
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class vntLogin : Page
    {
        private LogicaLogin logica;
        public vntLogin()
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
                string contrasenia = pwbContrasenia.Password;
                try
                {
                    Jugador jugador = logica.IniciarSesion(nombreUsuario, contrasenia);
                    if (jugador != null)
                    {
                        vntMenuPrincipal vntMenuPrincipal = new vntMenuPrincipal(jugador);
                        NavigationService.Navigate(vntMenuPrincipal);
                    }
                    else
                    {
                        mostrarAlerta("No se encontró al usuario, por favor revisa tu Nombre de Usuario o tu Contraseña");
                    }
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
                    Console.WriteLine(ex.Message);
                    mostrarAlerta("Lo sentimos, la comunicación con el servidor se anuló.");

                }
                catch (Exception ex)
                {
                    mostrarAlerta("Lo sentimos, ha ocurrido un error inesperado.");
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

            if (pwbContrasenia.Password.Equals(""))
            {
                tbcErrorContrasenia.Text = "Por favor, llena este campo";
                resultado = false;
            }

            return resultado;
        }

        private void mostrarAlerta(string mensaje)
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

        private void btnEntrarInvitado_Click(object sender, RoutedEventArgs e)
        {
            mostrarAlerta("¡Vaya! Parece que esta página aún no está terminada.");
        }

        private void btnCrearCuenta_Click(object sender, RoutedEventArgs e)
        {
            VntRegistrarUsuario vtnRegistrarUsuario = new VntRegistrarUsuario();
            NavigationService.Navigate(vtnRegistrarUsuario);
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
