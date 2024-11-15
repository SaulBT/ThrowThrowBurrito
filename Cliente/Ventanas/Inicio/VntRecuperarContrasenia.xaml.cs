using Cliente.ServiciosGestionUsuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace Cliente.Ventanas.Inicio
{
    /// <summary>
    /// Lógica de interacción para VntRecuperarContrasenia.xaml
    /// </summary>
    public partial class VntRecuperarContrasenia : Page
    {
        private bool operacionFueExitosa = false;
        private string correo;
        private string codigoRecuperacion;
        private ServicioCambiarContraseniaClient servicio;

        public VntRecuperarContrasenia()
        {
            InitializeComponent();
            servicio = new ServicioCambiarContraseniaClient();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                correo = servicio.VerificarExistenciaUsuario(txbNombreOCorreo.Text);
                if (correo != null)
                {
                    codigoRecuperacion = servicio.EnviarCorreoRecuperacion(correo);
                    tbcErrorNombreOCorreo.Visibility = Visibility.Hidden;
                    gFondoNegro.Visibility = Visibility.Visible;
                    gVentanaCodigo.Visibility = Visibility.Visible;
                }
                else
                {
                    tbcErrorNombreOCorreo.Visibility = Visibility.Visible;
                }
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

        //Ventana ingresar código

        private void btnAceptarCodigo_Click(object sender, RoutedEventArgs e)
        {
            if (txbCodigo.Text == codigoRecuperacion)
            {
                tbcErrorCodigo.Visibility = Visibility.Hidden;
                gVentanaContrasenia.Visibility = Visibility.Visible;
                gVentanaCodigo.Visibility = Visibility.Hidden;
            } 
            else
            {
                tbcErrorCodigo.Visibility = Visibility.Visible;
            }
        }

        private void btnCancelarCodigo_Click(object sender, RoutedEventArgs e)
        {
            gFondoNegro.Visibility = Visibility.Hidden;
            gVentanaCodigo.Visibility = Visibility.Hidden;
        }

        //Ventana cambiar contraseña

        private void btnAceptarContrasenia_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (validarDatos())
                {
                    string contraseniaHash = Utilidades.encriptar(pwbContrasenia.Password);
                    if (servicio.CambiarContrasenia(contraseniaHash, correo))
                    {
                        gVentanaContrasenia.Visibility = Visibility.Hidden;
                        operacionFueExitosa = true;
                        mostrarAlerta("La contraseña ha sido actualizada con éxito!");
                    }
                }
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


        private bool validarDatos()
        {
            bool contraseniaNuevaEsValida = ContraseniaEsValida(pwbContrasenia.Password);
            bool contraseniasSonIguales = ContraseniasSonIguales(pwbContrasenia.Password, pwbContraseniaConfirmacion.Password);

            if (contraseniaNuevaEsValida && contraseniasSonIguales)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ContraseniaEsValida(string contrasenia)
        {
            if ((contrasenia.Length >= 8) && (contrasenia.Length <= 100))
            {
                int contNumero = 0, contMayuscula = 0, contMinuscula = 0;
                foreach (char c in contrasenia)
                {
                    if (char.IsNumber(c))
                    {
                        contNumero++;
                    }
                    else if (char.IsUpper(c))
                    {
                        contMayuscula++;
                    }
                    else if (char.IsLower(c))
                    {
                        contMinuscula++;
                    }
                }
                if (contNumero > 0 && contMayuscula > 0 && contMinuscula > 0)
                {
                    tbcErrorContrasenia.Visibility = Visibility.Hidden;
                    return true;
                }
                else
                {
                    tbcErrorContrasenia.Visibility = Visibility.Visible;
                    return false;
                }
            }
            else
            {
                tbcErrorContrasenia.Visibility = Visibility.Visible;
                return false;
            }
        }

        public bool ContraseniasSonIguales(string contrasenia, string contraseniaConfirmacion)
        {
            if (contrasenia == contraseniaConfirmacion)
            {
                tbcErrorConfirmacion.Visibility = Visibility.Hidden;
                return true;
            }
            else
            {
                tbcErrorConfirmacion.Visibility = Visibility.Visible;  
                return false;
            }
        }


        private void btnCancelarContrasenia_Click(object sender, RoutedEventArgs e)
        {
            gVentanaCodigo.Visibility= Visibility.Visible;
            gVentanaContrasenia.Visibility = Visibility.Hidden;

        }

        //Ventana emergente

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

    }
}
